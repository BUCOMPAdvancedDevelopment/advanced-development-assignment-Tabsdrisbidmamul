export interface IUserDTO {
  displayName: string;
  token: string;
  username: string;
  image: IImage | null;
  role: string;
}

export interface IImage {
  id: string;
  publicId: string;
  url: string;
}

export interface IPasswordChange {
  oldPassword: string;
  newPassword: string;
}

export interface IUserDisplayNameAndImage {
  image: IImage;
  displayName: string;
}

export interface IUserDisplayNameAndImageAndReviewsRating {
  displayName: string;
  image: IImage;
  review: string;
  rating: number;
}

export class PasswordChange implements IPasswordChange {
  oldPassword: string;
  newPassword: string;

  constructor(oldPassword: string, newPassword: string) {
    this.oldPassword = oldPassword;
    this.newPassword = newPassword;
  }
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
