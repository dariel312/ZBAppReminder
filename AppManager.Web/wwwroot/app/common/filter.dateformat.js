const DateFormatFilter = function ($filter) {
    return function (date, format, timezone) {

        if (timezone === null || timezone === undefined)
            timezone = 'UTC';

        return $filter('date')(date, format, timezone);
    };
}