function helperButtonForToolbar(){
    let path = window.location.pathname;
    let splitPath = path.split('/');
    let controller = splitPath[1];
    let action = splitPath[2] || '';
    let url = `/Help/Help?CurrentLocation=/${controller}`;
    if (action!=='') url = url+ `/${action}`;
    return {
        type: 'button',
        id: 'helpButton',
        text: '',
        icon: 'question',
        url: url,
        attributes: {'target': '_blank'}
    };
}