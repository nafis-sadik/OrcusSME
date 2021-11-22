using DataLayer;
using Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DataLayer.Entities;
using DataLayer.MySql;
using Microsoft.EntityFrameworkCore;
using Repositories.Implementation;
using Services.Orcus.Abstraction;
using DataLayer.MSSQL;

namespace Services.Orcus.Implementation
{
    public class OutletManagerService : IOutletManagerService
    {
        private readonly IOutletManagerRepo _outletManagerRepo;
        private readonly ICrashLogRepo _crashLogRepo;

        public OutletManagerService()
        {
            OrcusSMEContext context = new OrcusSMEContext(new DbContextOptions<OrcusSMEContext>());
            _outletManagerRepo = new OutletManagerRepo(context);
            _crashLogRepo = new CrashLogRepo(context);
        }

        public List<Models.Outlet> AddOutlet(Models.Outlet outlet)
        {
            List<Models.Outlet> response = new List<Models.Outlet>();
            try
            {
                int pk = 0;
                if (_outletManagerRepo.AsQueryable().Any())
                    pk = (int)_outletManagerRepo.AsQueryable().Max(x => x.OutletId) + 1;

                bool status = _outletManagerRepo.Add(new Outlet
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
                if (_crashLogRepo.AsQueryable().Any())
                    pk = 0;
                else
                    pk = _crashLogRepo.AsQueryable().Max(x => x.CrashLogId) + 1;

                if (ex.InnerException != null)
                    _crashLogRepo.Add(new Crashlog
                    {
                        CrashLogId = pk,
                        ClassName = "OutletManagerService",
                        MethodName = "AddOutlet",
                        ErrorMessage = ex.Message,
                        ErrorInner =
                            (string.IsNullOrEmpty(ex.Message) || ex.Message == CommonConstants.MsgInInnerException
                                ? ex.InnerException.Message
                                : ex.Message),
                        Data = outlet.UserId,
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
                if (oldData != null)
                {
                    oldData.Status = CommonConstants.StatusTypes.Archived;
                    _outletManagerRepo.Update(oldData);
                }

                response = _outletManagerRepo.AsQueryable().Where(x => x.UserId == outlet.UserId && x.Status == CommonConstants.StatusTypes.Active).Select(x => new Models.Outlet { OutletId = x.OutletId, OutletName = x.OutletName }).ToList();
            }
            catch (Exception ex)
            {
                _outletManagerRepo.Rollback();

                int pk;
                if (!_crashLogRepo.AsQueryable().Any())
                    pk = 0;
                else
                    pk = _crashLogRepo.AsQueryable().Max(x => x.CrashLogId) + 1;

                if (ex.InnerException != null)
                    if (oldData != null)
                        _crashLogRepo.Add(new Crashlog
                        {
                            CrashLogId = pk,
                            ClassName = "OutletManagerService",
                            MethodName = "ArchiveOutlet",
                            ErrorMessage = ex.Message,
                            ErrorInner =
                                (string.IsNullOrEmpty(ex.Message) || ex.Message == CommonConstants.MsgInInnerException
                                    ? ex.InnerException.Message
                                    : ex.Message),
                            Data = oldData.UserId,
                            TimeStamp = DateTime.Now
                        });
            }

            return response;
        }

        public Models.Outlet GetOutlet(decimal outletId)
        {
            Models.Outlet response;
            try
            {
                Outlet outlet = _outletManagerRepo.Get(outletId);
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
                if (_crashLogRepo.AsQueryable().Any())
                    pk = 0;
                else
                    pk = _crashLogRepo.AsQueryable().Max(x => x.CrashLogId) + 1;

                if (ex.InnerException != null)
                    _crashLogRepo.Add(new Crashlog
                    {
                        CrashLogId = pk,
                        ClassName = "OutletManagerService",
                        MethodName = "GetOutlet",
                        ErrorMessage = ex.Message,
                        ErrorInner =
                            (string.IsNullOrEmpty(ex.Message) || ex.Message == CommonConstants.MsgInInnerException
                                ? ex.InnerException.Message
                                : ex.Message),
                        Data = outletId.ToString(NumberFormatInfo.CurrentInfo),
                        TimeStamp = DateTime.Now
                    });
                response = null;
            }

            return response;
        }

        public List<Models.Outlet> GetOutletsByUserId(string userId)
        {
            List<Models.Outlet> response = null;
            try
            {
                response = _outletManagerRepo.AsQueryable().Where(x => x.UserId == userId && x.Status == CommonConstants.StatusTypes.Active).Select(x => new Models.Outlet { OutletId = x.OutletId, OutletName = x.OutletName }).ToList();
            }
            catch (Exception ex)
            {
                _outletManagerRepo.Rollback();

                int pk;
                if (_crashLogRepo.AsQueryable().Any())
                    pk = 0;
                else
                    pk = _crashLogRepo.AsQueryable().Max(x => x.CrashLogId) + 1;

                if (ex.InnerException != null)
                    _crashLogRepo.Add(new Crashlog
                    {
                        CrashLogId = pk,
                        ClassName = "OutletManagerService",
                        MethodName = "GetOutletsByUserId",
                        ErrorMessage = ex.Message,
                        ErrorInner =
                            (string.IsNullOrEmpty(ex.Message) || ex.Message == CommonConstants.MsgInInnerException
                                ? ex.InnerException.Message
                                : ex.Message),
                        Data = userId,
                        TimeStamp = DateTime.Now
                    });
            }
            return response;
        }

        public bool? OrderSite(decimal outletId, out string response)
        {
            try
            {
                Outlet outletData = _outletManagerRepo.Get(outletId);

                if (!string.IsNullOrEmpty(outletData.SiteUrl))
                {
                    response = "You already have a site for this outlet</br>Visit : " + outletData.SiteUrl;
                    return false;
                }

                outletData.RequestSite = 1; // 1 in mssql means true in mysql and 0 means false
                _outletManagerRepo.Update(outletData);
                response = "Site order placed successfully";
                return true;
            }
            catch (Exception ex)
            {
                _outletManagerRepo.Rollback();

                int pk;
                if (_crashLogRepo.AsQueryable().Any())
                    pk = 0;
                else
                    pk = _crashLogRepo.AsQueryable().Max(x => x.CrashLogId) + 1;

                if (ex.InnerException != null)
                    _crashLogRepo.Add(new Crashlog
                    {
                        CrashLogId = pk,
                        ClassName = "OutletManagerService",
                        MethodName = "OrderSite",
                        ErrorMessage = ex.Message,
                        ErrorInner = 
                            (string.IsNullOrEmpty(ex.Message) || ex.Message == CommonConstants.MsgInInnerException
                                ? ex.InnerException.Message
                                : ex.Message),
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

                if (oldData != null)
                {
                    oldData.OutletAddresss = outlet.OutletAddresss;
                    oldData.OutletName = outlet.OutletName;
                    oldData.UserId = outlet.UserId;

                    _outletManagerRepo.Update(oldData);
                }

                response = _outletManagerRepo.AsQueryable().Where(x => x.UserId == outlet.UserId && x.Status == CommonConstants.StatusTypes.Active).Select(x => new Models.Outlet { OutletId = x.OutletId, OutletName = x.OutletName }).ToList();
            }
            catch (Exception ex)
            {
                _outletManagerRepo.Rollback();

                int pk;
                if (_crashLogRepo.AsQueryable().Any())
                    pk = 0;
                else
                    pk = _crashLogRepo.AsQueryable().Max(x => x.CrashLogId) + 1;

                if (ex.InnerException != null)
                    _crashLogRepo.Add(new Crashlog
                    {
                        CrashLogId = pk,
                        ClassName = "OutletManagerService",
                        MethodName = "UpdateOutlet",
                        ErrorMessage = ex.Message,
                        ErrorInner =
                            (string.IsNullOrEmpty(ex.Message) || ex.Message == CommonConstants.MsgInInnerException
                                ? ex.InnerException.Message
                                : ex.Message),
                        Data = outlet.UserId,
                        TimeStamp = DateTime.Now
                    });
            }

            return response;
        }
    }
}
