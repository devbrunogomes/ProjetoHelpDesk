import { jwtDecode } from "jwt-decode";

export function verificarRoleDoToken(token) {
  const decodedToken = jwtDecode(token);
  const roleClaimKey =
    "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
  const userRole = decodedToken[roleClaimKey];

  return userRole;
}
