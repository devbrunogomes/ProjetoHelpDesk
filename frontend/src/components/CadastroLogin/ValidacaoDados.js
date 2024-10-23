import axios from "axios";

export function validarNomeCompleto(nomeCompleto) {
  // Verifica se o nome contém apenas letras e espaços
  const regex = /^[a-zA-Zá-úÁ-ÚÃ-ÕâêîôûÂÊÎÔÛ\s]+$/;

  // Verifica se o nome tem mais de 15 caracteres e passa na validação da regex
  return nomeCompleto.length > 15 && regex.test(nomeCompleto);
}

export function validarIgualdadeEmail(email, reEmail) {
  return email === reEmail; // Emails são iguais
}

export function validarUsername(username) {
  // Verifica se o username tem no mínimo 8 caracteres
  const minLength = 8;
  
  // Verifica se contém pelo menos uma letra maiúscula
  const hasUppercase = /[A-Z]/;

  // Retorna true se ambas as condições forem atendidas
  return username.length >= minLength && hasUppercase.test(username);
}

export function validarIgualdadeSenhas(password, rePassword) {
  return password === rePassword; // Senhas são iguais
}

export function validarSenha(password) {
  // Regras de validação conforme os padrões do Identity
  const minLength = 8;
  const requireDigit = /\d/; // Pelo menos um dígito
  const requireUppercase = /[A-Z]/; // Pelo menos uma letra maiúscula
  const requireLowercase = /[a-z]/; // Pelo menos uma letra minúscula

  // Verifica se a senha atende a todos os critérios
  if (
    password.length < minLength ||
    !requireDigit.test(password) ||
    !requireUppercase.test(password) ||
    !requireLowercase.test(password)
  ) {
    return false;
  }

  // Verifica se a senha contém pelo menos um caractere único (não repetido)
  const uniqueChars = new Set(password);

  if (uniqueChars.size < password.length - 1) {
    return false;
  }

  return true; // Senhas são válidas e iguais
}
