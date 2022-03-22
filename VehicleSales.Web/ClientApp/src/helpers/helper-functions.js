export function formatToCAD(num) {
    var p = num.toFixed(2).split(".");
    return "CAD$" + p[0].split("").reverse().reduce(function(acc, num, i, orig) {
        return num + (num !== "-" && i && !(i % 3) ? "," : "") + acc;
    }, "") + "." + p[1];
}

export function groupBy(objectArray, property) {
    return objectArray.reduce((acc, obj) => {
        const key = obj[property];
        if (!acc[key]) {
            acc[key] = [];
        }
        // Add object to list for given key's value
        acc[key].push(obj);
        return acc;
    }, {});
}