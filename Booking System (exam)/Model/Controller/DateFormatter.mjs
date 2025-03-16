export function dateFormatter(dato) {
    //Get date from function and make it a new date
    const startDato = new Date(dato);

    //Get specific date without timestamp
    const day = startDato.getDate().toString().padStart(2, '0'); // Gets day and makes sure it's 2 digit
    const month = (startDato.getMonth() + 1).toString().padStart(2, '0'); // gets month (Notice: January is month 0)
    const year = startDato.getFullYear(); // gets year

    //Format date to use in database
    const formattedDate = `${day}${month}${year}`;
    return formattedDate;
    //Ex. 02 feb 2023 returns as 02022023
}