import { jwtDecode } from "jwt-decode";

export function verificarRoleDoToken(token) {
  const decodedToken = jwtDecode(token);
  const userRole =
    decodedToken[
      "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
    ];
  console.log(decodedToken);
  console.log(userRole);
}
