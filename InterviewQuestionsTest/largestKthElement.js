function Sort(arr) {
    var sorted = "";

    if (arr !== null || arr !== undefined) {
        // Find lowest
        for (;arr.length !== 0;) {
            var temp = null;
            for (var j = 0; j < arr.length; j++) {
                if (temp === null || arr[temp] > arr[j]) {
                    temp = j;
                }
            }
            
            sorted += arr[temp];
            arr.splice(temp, 1);
        }
    }

    return sorted;
};

function GetLargestKthElement(k, arr) {
    var temp = Sort(arr);
    return temp[temp.length - k];
};

var string = "157438925";

var array = [];
for (var i in string) {
    array.push(string[i]);
}

console.log(GetLargestKthElement(3, array));
