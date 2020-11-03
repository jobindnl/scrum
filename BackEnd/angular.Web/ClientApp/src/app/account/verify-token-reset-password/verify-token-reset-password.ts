export interface IVerifyTokenResetPassword {
  newPwd: string;
  confirmPwd: string;
  token: string;
  email: string;
}
