
const changeUrlQuery = (tab) => {

    if ('URLSearchParams' in window) {

        var searchParams = new URLSearchParams(window.location.search);

        if (!tab) {
            searchParams.delete('t')
        }
        else {
            searchParams.set("t", tab);
        }

        let newRelativePathQuery = window.location.pathname + '?' + searchParams.toString();

        history.pushState(null, '', newRelativePathQuery);
    }
}


$(document).ready(function () {

    var paramTab = getParam('t')

    if (paramTab) {

        const tabElemenet = $(`.nav-tabs a[data-code="${paramTab}"]`)

        if (tabElemenet) {
            tabElemenet.tab('show')
        }
    }

    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        var target = $(e.target).attr("data-code")

        changeUrlQuery(target)
    });

})



