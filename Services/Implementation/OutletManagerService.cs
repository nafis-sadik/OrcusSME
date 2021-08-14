using DataLayer;
using Entities;
using Repositories;
using Repositories.Abstraction;
using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class OutletManagerService : IOutletManagerService
    {
        private readonly IOutletManagerRepo _outletManagerRepo;
        private readonly ICrashLogRepo _crashLogRepo;

        public OutletManagerService(IOutletManagerRepo outletManagerRepo, ICrashLogRepo crashLogRepo)
        {
            _outletManagerRepo = outletManagerRepo;
            _crashLogRepo = crashLogRepo;
        }

        public List<Models.Outlet> AddOutlet(Models.Outlet outlet)
        {
            List<Models.Outlet> response = new List<Models.Outlet>();
            try
            {
                int pk = 0;
                if (_outletManagerRepo.AsQueryable().Count() > 0)
                    pk = (int)_outletManagerRepo.AsQueryable().Max(x => x.OutletId) + 1;

                bool status = _outletManagerRepo.RegisterNewOutlet(new Outlet
                {
                    OutletAddresss = outlet.OutletAddresss,
                    OutletName = outlet.OutletName,
                    UserId = outlet.UserId,
                    OutletId = pk,
                    Status = CommonConstants.StatusTypes.Active
                });

                if (status)
                    response = _outletManagerRepo.AsQueryable().Where(x => x.UserId == outlet.UserId && x.Status == CommonConstants.StatusTypes.Active).Select(x => new Models.Outlet { OutletId = x.OutletId, OutletName = x.OutletName }).ToList();
                else
                    return null;
            }
            catch (Exception ex)
            {
                _outletManagerRepo.Rollback();

                int pk;
                if (_crashLogRepo.AsQueryable().Count() <= 0)
                    pk = 0;
                else
                    pk = _crashLogRepo.AsQueryable().Max(x => x.CrashLogId) + 1;

                _crashLogRepo.Add(new CrashLog
                {
                    CrashLogId = pk,
                    ClassName = "OutletManagerService",
                    MethodName = "AddOutlet",
                    ErrorMessage = ex.Message.ToString(),
                    ErrorInner = (string.IsNullOrEmpty(ex.Message) || ex.Message == CommonConstants.MsgInInnerException ? ex.InnerException.Message : ex.Message).ToString(),
                    Data = outlet.UserId.ToString(),
                    TimeStamp = DateTime.Now
                });
            }

            return response;
        }

        public List<Models.Outlet> ArchiveOutlet(Models.Outlet outlet)
        {
            List<Models.Outlet> response = new List<Models.Outlet>();
            Outlet oldData = _outletManagerRepo.AsQueryable().FirstOrDefault(x => x.OutletId == outlet.OutletId);
            try
            {
                oldData.Status = CommonConstants.StatusTypes.Archived;
                _outletManagerRepo.Update(oldData);

                response = _outletManagerRepo.AsQueryable().Where(x => x.UserId == outlet.UserId && x.Status == CommonConstants.StatusTypes.Active).Select(x => new Models.Outlet { OutletId = x.OutletId, OutletName = x.OutletName }).ToList();
            }
            catch (Exception ex)
            {
                _outletManagerRepo.Rollback();

                int pk;
                if (_crashLogRepo.AsQueryable().Count() <= 0)
                    pk = 0;
                else
                    pk = _crashLogRepo.AsQueryable().Max(x => x.CrashLogId) + 1;

                _crashLogRepo.Add(new CrashLog
                {
                    CrashLogId = pk,
                    ClassName = "OutletManagerService",
                    MethodName = "ArchiveOutlet",
                    ErrorMessage = ex.Message,
                    ErrorInner = (string.IsNullOrEmpty(ex.Message) || ex.Message == CommonConstants.MsgInInnerException ? ex.InnerException.Message : ex.Message),
                    Data = oldData.UserId,
                    TimeStamp = DateTime.Now
                });
            }

            return response;
        }

        public Models.Outlet GetOutlet(decimal OutletId)
        {
            Models.Outlet response;
            try
            {
                Outlet outlet = _outletManagerRepo.Get(OutletId);
                response = new Models.Outlet
                {
                    OutletId = outlet.OutletId,
                    OutletName = outlet.OutletName,
                    OutletAddresss = outlet.OutletAddresss,
                    UserId = outlet.UserId
                };
            }
            catch (Exception ex)
            {
                _outletManagerRepo.Rollback();

                int pk;
                if (_crashLogRepo.AsQueryable().Count() <= 0)
                    pk = 0;
                else
                    pk = _crashLogRepo.AsQueryable().Max(x => x.CrashLogId) + 1;

                _crashLogRepo.Add(new CrashLog
                {
                    CrashLogId = pk,
                    ClassName = "OutletManagerService",
                    MethodName = "GetOutlet",
                    ErrorMessage = ex.Message,
                    ErrorInner = (string.IsNullOrEmpty(ex.Message) || ex.Message == CommonConstants.MsgInInnerException ? ex.InnerException.Message : ex.Message),
                    Data = OutletId.ToString(),
                    TimeStamp = DateTime.Now
                });
                response = null;
            }

            return response;
        }

        public List<Models.Outlet> GetOutletsByUserId(string UserId)
        {
            List<Models.Outlet> response = new List<Models.Outlet>();
            try
            {
                response = _outletManagerRepo.AsQueryable().Where(x => x.UserId == UserId && x.Status == CommonConstants.StatusTypes.Active).Select(x => new Models.Outlet { OutletId = x.OutletId, OutletName = x.OutletName }).ToList();
            }
            catch (Exception ex)
            {
                _outletManagerRepo.Rollback();

                int pk;
                if (_crashLogRepo.AsQueryable().Count() <= 0)
                    pk = 0;
                else
                    pk = _crashLogRepo.AsQueryable().Max(x => x.CrashLogId) + 1;

                _crashLogRepo.Add(new CrashLog
                {
                    CrashLogId = pk,
                    ClassName = "OutletManagerService",
                    MethodName = "GetOutletsByUserId",
                    ErrorMessage = ex.Message,
                    ErrorInner = (string.IsNullOrEmpty(ex.Message) || ex.Message == CommonConstants.MsgInInnerException ? ex.InnerException.Message : ex.Message),
                    Data = UserId,
                    TimeStamp = DateTime.Now
                });
            }
            return response;
        }

        public bool? OrderSite(decimal OutletId, out string response)
        {
            try
            {
                Outlet OutletData = _outletManagerRepo.Get(OutletId);

                if (!string.IsNullOrEmpty(OutletData.SiteUrl))
                {
                    response = "You already have a site for this outlet</br>Visit : " + OutletData.SiteUrl;
                    return false;
                }

                OutletData.RequestSite = true;
                _outletManagerRepo.Update(OutletData);
                response = "Site order placed successfully";
                return true;
            }
            catch (Exception ex)
            {
                _outletManagerRepo.Rollback();

                int pk;
                if (_crashLogRepo.AsQueryable().Count() <= 0)
                    pk = 0;
                else
                    pk = _crashLogRepo.AsQueryable().Max(x => x.CrashLogId) + 1;

                _crashLogRepo.Add(new CrashLog
                {
                    CrashLogId = pk,
                    ClassName = "OutletManagerService",
                    MethodName = "OrderSite",
                    ErrorMessage = ex.Message,
                    ErrorInner = (string.IsNullOrEmpty(ex.Message) || ex.Message == CommonConstants.MsgInInnerException ? ex.InnerException.Message : ex.Message),
                    Data = null,
                    TimeStamp = DateTime.Now
                });
                response = "An internal error has occured";
                return null;
            }
        }

        public List<Models.Outlet> UpdateOutlet(Models.Outlet outlet)
        {
            List<Models.Outlet> response = new List<Models.Outlet>();
            try
            {
                Outlet oldData = _outletManagerRepo.AsQueryable().FirstOrDefault(x => x.OutletId == outlet.OutletId);

                oldData.OutletAddresss = outlet.OutletAddresss;
                oldData.OutletName = outlet.OutletName;
                oldData.UserId = outlet.UserId;

                _outletManagerRepo.Update(oldData);

                response = _outletManagerRepo.AsQueryable().Where(x => x.UserId == outlet.UserId && x.Status == CommonConstants.StatusTypes.Active).Select(x => new Models.Outlet { OutletId = x.OutletId, OutletName = x.OutletName }).ToList();
            }
            catch (Exception ex)
            {
                _outletManagerRepo.Rollback();

                int pk;
                if (_crashLogRepo.AsQueryable().Count() <= 0)
                    pk = 0;
                else
                    pk = _crashLogRepo.AsQueryable().Max(x => x.CrashLogId) + 1;

                _crashLogRepo.Add(new CrashLog
                {
                    CrashLogId = pk,
                    ClassName = "OutletManagerService",
                    MethodName = "UpdateOutlet",
                    ErrorMessage = ex.Message,
                    ErrorInner = (string.IsNullOrEmpty(ex.Message) || ex.Message == CommonConstants.MsgInInnerException ? ex.InnerException.Message : ex.Message),
                    Data = outlet.UserId,
                    TimeStamp = DateTime.Now
                });
            }

            return response;
        }
    }
}
