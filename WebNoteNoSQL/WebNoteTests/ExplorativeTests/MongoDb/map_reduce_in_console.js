var map = function() {
    var logEntry = this;
    emit(logEntry.Country,
        { count: 1, size: logEntry.Size });
};

var reduce = function(key, values) {
    var result = { count: 0, totalMinutes: 0 };

    values.forEach(function(value) {
        result.count += value.count;
        result.size += value.size;
    });

    return result;
};

db.logs.mapReduce(map, reduce, { out: 'log_result' });
db.log_result.find();
