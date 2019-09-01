// для представлений Tenements\LivingSpace и
// Fsp\Details
function onPaymentsGridDataBound(e) {
    var grid = this;
    var items = grid.items();
    var columnsCount = grid.columns.length;

    items.each(function (index) {
        var dataItem = grid.dataItem(this);

        if (dataItem.FlagInsur) {
            var $td_month = $(this).find('.fsp_payment_month');

            if ($td_month)
                $td_month.addClass('k-grid-opl');
        }
    });

    grid.element.find(".k-grouping-row").each(function (e) {
        grid.collapseGroup(this);

        $(this).find('td').each(function (e) {
            var colspan = $(this).attr('colspan');
            if (colspan) {
                if (colspan - columnsCount == 0)
                    $(this).attr('colspan', 2);
            }
        });
    });

    grid.expandRow(this.tbody.find('tr.k-grouping-row').first());
}