export function setDate(date) {
    //Checks if sesson storage is supported by the browser
    if (typeof (Storage) !== "undefined") {
        sessionStorage.setItem("Dato", date);
    } else {
        console.log("Local Storage not supported!");
    }
}

export function getDate() {
    var tempdate = sessionStorage.getItem("Dato");
    //Returns date in the format: yyyy-mm-dd
    return tempdate;
}