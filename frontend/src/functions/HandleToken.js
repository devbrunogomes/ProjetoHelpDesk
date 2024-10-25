import { jwtDecode } from "jwt-decode";

export function verificarRoleDoToken(token) {
  const decodedToken = jwtDecode(token);
  const roleClaimKey =
    "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
  const userRole = decodedToken[roleClaimKey];

  return userRole;
}

export function retornarUsernameDoToken() {
  const token = localStorage.getItem("token");
  const decodedToken = jwtDecode(token);
  const usernameClaimKey =
    "username";
  const username = decodedToken[usernameClaimKey];

  return username;
}