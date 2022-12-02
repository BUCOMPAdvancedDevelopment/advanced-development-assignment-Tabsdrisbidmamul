export interface IUserDTO {
  displayName: string;
  token: string;
  username: string;
  image: {
    id: string;
    publicId: string;
    url: string;
  };
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

export interface ISignupDTO {
  displayName: string;
  email: string;
  password: string;
  username: string;
}

export class SignupDTO implements ISignupDTO {
  displayName: string;
  email: string;
  password: string;
  username: string;

  constructor(
    displayName: string,
    email: string,
    password: string,
    username: string
  ) {
    this.displayName = displayName;
    this.email = email;
    this.password = password;
    this.username = username;
  }
}
