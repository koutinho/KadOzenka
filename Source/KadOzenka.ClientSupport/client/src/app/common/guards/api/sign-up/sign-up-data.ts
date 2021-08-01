export class SignUpData {
    public login: string;
    public email: string;
    public password: string;

    constructor(login: string, email: string, password: string) {
        this.login = login;
        this.email = email;
        this.password = password;
    }
}