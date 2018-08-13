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

function StringSort(incomingStringToSort) {
    var lowerCase = "";
    var upperCase = "";
    var digits = "";

    var lowerCasePatt = new RegExp("[a-z]", "g");
    var upperCasePatt = new RegExp("[A-Z]", "g");
    var digitsPatt = new RegExp("[0-9]", "g");
    
    lowerCase = incomingStringToSort.match(lowerCasePatt);
    upperCase = incomingStringToSort.match(upperCasePatt);
    digits = incomingStringToSort.match(digitsPatt);

    var evenDigits = [];
    var oddDigits = [];
    for (var i in digits) {
        if ((digits[i] % 2) !== 0) { 
            oddDigits.push(digits[i]);
        } else {
            evenDigits.push(digits[i]);
        }
    }
    return Sort(lowerCase) + Sort(upperCase) + Sort(evenDigits) + Sort(oddDigits);
};

console.log(Date.now());
var myString = "ba4321bbCBA";

console.log(StringSort(myString));

console.log(Date.now());