NestableInit = (SerializerId) => {
    if(SerializerId != '' && SerializerId[0] != '#') { SerializerId = '#' + SerializerId; }
    // Generate UI
    $('.dd').nestable();

    // Fill json output text area
    let serializedData = window.JSON.stringify($('.dd').nestable('serialize'));
    if(SerializerId != '') {
        $(SerializerId).val(serializedData);
    }

    // Detatch all functions from event
    $('.dd').off('change')

    // Attatch function to event
    $('.dd').on('change', function () {
        let $this = $(this);
        let serializedData = window.JSON.stringify($($this).nestable('serialize'));
        if(SerializerId != '') {
            $(SerializerId).val(serializedData);
        }
        
        // $this.parents('div.body').find('textarea').val(serializedData);
    });
}