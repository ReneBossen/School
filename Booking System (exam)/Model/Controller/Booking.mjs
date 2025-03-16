export class Booking {
    #status;
    #firstName;
    #lastName;
    #phone;
    #mail;
    #classNr;
    #education;
    #participants;
    #start;
    #end;
    #description;
    #requests;
    #miscellaneous;

    //Create constructor for booking object
    constructor(status, firstName, lastName, phone, mail, classNr, education, participants, start, end, description, requests, miscellaneous) {
        this.#status = status;
        this.#firstName = firstName;
        this.#lastName = lastName;
        this.#phone = phone;
        this.#mail = mail;
        this.#classNr = classNr;
        this.#education = education;
        this.#participants = participants;
        this.#start = start;
        this.#end = end;
        this.#description = description;
        this.#requests = requests;
        this.#miscellaneous = miscellaneous;
    }

    //toObject function, for express Server
    toObject() {
        return {
            status: this.#status,
            firstName: this.#firstName,
            lastName: this.#lastName,
            phone: this.#phone,
            mail: this.#mail,
            classNr: this.#classNr,
            education: this.#education,
            participants: this.#participants,
            start: this.#start,
            end: this.#end,
            description: this.#description,
            requests: this.#requests,
            miscellaneous: this.#miscellaneous
        };

    }

    //toString function
    toString() {
        return this.#status + ", " +
            this.#firstName + ", " +
            this.#lastName + ", " +
            this.#phone + ", " +
            this.#mail + ", " +
            this.#classNr + ", " +
            this.#education + ", " +
            this.#participants + ", " +
            this.#start + ", " +
            this.#end + ", " +
            this.#description + ", " +
            this.#requests + ", " +
            this.#miscellaneous;
    }

    setStart(time) {
        this.#start = time;
    }

    setEnd(time) {
        this.#end = time;
    }

    setStatus(status) {
        this.#status = status;
    }

    getStatus() {
        return this.#status;
    }

    getFirstName() {
        return this.#firstName;
    }

    getLastName() {
        return this.#lastName;
    }

    getPhone() {
        return this.#phone;
    }

    getMail() {
        return this.#mail;
    }

    getClassNr() {
        return this.#classNr;
    }

    getEducation() {
        return this.#education;
    }

    getParticipants() {
        return this.#participants;
    }

    getStart() {
        return this.#start;
    }

    getEnd() {
        return this.#end;
    }

    getDescription() {
        return this.#description;
    }

    getRequests() {
        return this.#requests;
    }

    getMiscellaneous() {
        return this.#miscellaneous;
    }
}