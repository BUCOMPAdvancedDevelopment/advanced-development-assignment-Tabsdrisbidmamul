export interface IUserDTO {
  displayName: string;
  token: string;
  username: string;
  image: string | null;
  role: string;
}

export interface ILoginDTO {
  email: string;
  password: string;
}

export class LoginDTO implements ILoginDTO {
  email: string;
  password: string;

  constructor(login: string, password: string) {
    this.email = login;
    this.password = password;
  }
}
