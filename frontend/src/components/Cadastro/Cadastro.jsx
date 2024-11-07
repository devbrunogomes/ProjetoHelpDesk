import { useState } from "react";
import styles from "./styles.module.scss";
import axios from "axios";
import * as validacao from "../../functions/ValidacaoDados";

export const Cadastro = (props) => {
  //Cadastro Variaveis
  const [nome, setNome] = useState("");
  const [email, setEmail] = useState("");
  const [confirmarEmail, setConfirmarEmail] = useState("");
  const [cep, setCep] = useState("");
  const [usernameCadastro, setUsernameCadastro] = useState("");
  const [passwordCadastro, setPasswordCadastro] = useState("");
  const [confirmarSenhaCadastro, setConfirmarSenhaCadastro] = useState("");

  //Cep Variaveis
  const [msgCep, setMsgCep] = useState("Insira um cep válido");
  const [cepIsValid, setCepIsValid] = useState(false);

  //Msg de confirmação
  const [msgConfirmacao, setMsgConfirmacao] = useState("");

  //Feedback Visual
  const [visualEmailValidacao, setVisualEmailValidacao] = useState({});
  const [visualReEmailValidacao, setVisualReEmailValidacao] = useState({});
  const [visualUsernameValidacao, setVisualUsernameValidacao] = useState({});
  const [visualCepValidacao, setVisualCepValidacao] = useState({});
  const estiloItemValidado = { borderBottom: "2px solid rgb(95, 226, 112)" };
  const estiloItemInvalidado = { borderBottom: "2px solid rgb(219, 34, 34)" };

  const handleCadastro = async (event) => {
    event.preventDefault();

    if (!validarDados()) {
      return;
    }

    try {
      // Fazer a requisição POST para o backend
      const response = await axios.post("http://localhost:5089/Cliente", {
        nomeCompleto: nome,
        email: email,
        emailConfirmation: confirmarEmail,
        userName: usernameCadastro,
        password: passwordCadastro,
        rePassword: confirmarSenhaCadastro,
        cep: cep,
      });

      console.log(response);

      setMsgConfirmacao(response.data);
      reiniciarFormulario();
    } catch (error) {
      console.error(error);
      setMsgConfirmacao(error.response.data);
    }
  };

  const validarDados = () => {
    if (!validacao.validarNomeCompleto(nome)) {
      alert("Nome Completo inválido");
      return false;
    }

    if (!validacao.validarIgualdadeEmail(email, confirmarEmail)) {
      alert("Emails são diferentes");
      return false;
    }

    if (!cepIsValid) {
      alert("CEP Inválido");
      return false;
    }

    if (!validacao.validarUsername(usernameCadastro)) {
      alert("Username Inválido");
      return false;
    }

    if (
      !validacao.validarIgualdadeSenhas(
        passwordCadastro,
        confirmarSenhaCadastro
      )
    ) {
      alert("Senhas são diferentes");
      return false;
    }

    if (!validacao.validarSenha(passwordCadastro)) {
      alert(
        "Senha precisa ter:  entre 8 e 16 caracteres, incluindo letras e números"
      );
      return false;
    }

    return true;
  };

  const reiniciarFormulario = () => {
    setNome("");
    setEmail("");
    setConfirmarEmail("");
    setCep("");
    setUsernameCadastro("");
    setPasswordCadastro("");
    setConfirmarSenhaCadastro("");
    setMsgCep("Insira um cep válido");
    setCepIsValid(false);
    setVisualEmailValidacao({});
    setVisualUsernameValidacao({});
    setVisualCepValidacao({});
  };

  const handleEmail = async (event) => {
    const email = event.target.value;
    setEmail(email);
    setVisualEmailValidacao({});

    if (email.indexOf("@") !== -1 && email.indexOf(".com") !== -1) {
      try {
        const response = await axios.get(
          "http://localhost:5089/api/Acesso/validar-email",
          {
            params: {
              email,
            },
          }
        );
        setVisualEmailValidacao(estiloItemValidado);
      } catch (error) {
        console.log(`Falha na requisicao para validar email: ${error}`);
        setVisualEmailValidacao(estiloItemInvalidado);
      }
    }
  };

  const handleConfirmarEmail = (event) => {
    const reEmail = event.target.value;
    setConfirmarEmail(reEmail);
    setVisualReEmailValidacao({});

    if (reEmail.indexOf("@") !== -1 && reEmail.indexOf(".com") !== -1) {
      if (reEmail === email) {
        setVisualReEmailValidacao(estiloItemValidado);
      } else {
        setVisualReEmailValidacao(estiloItemInvalidado);
      }
    }

    
  };

  const handleUsername = async (event) => {
    const username = event.target.value;
    setUsernameCadastro(username);
    setVisualUsernameValidacao({});

    if (validacao.validarUsername(username)) {
      try {
        const response = await axios.get(
          "http://localhost:5089/api/Acesso/validar-username",
          {
            params: {
              username: username,
            },
          }
        );
        setVisualUsernameValidacao(estiloItemValidado);
      } catch (error) {
        console.log(`Falha na requisicao para validar username: ${error}`);
        setVisualUsernameValidacao(estiloItemInvalidado);
      }
    }
  };

  const handleCep = async (event) => {
    const cep = event.target.value.replace(/\D/g, ""); // Remove caracteres não numéricos
    setCep(cep);

    if (cep.length === 8) {
      try {
        const response = await fetch(`https://viacep.com.br/ws/${cep}/json/`);
        const data = await response.json();

        if (data.erro) {
          setMsgCep("CEP não encontrado.");
          setCepIsValid(false);
          setVisualCepValidacao(estiloItemInvalidado);
          event.target.focus();
        } else {
          setMsgCep(`${data.localidade} - ${data.uf}`);
          setCepIsValid(true);
          setVisualCepValidacao(estiloItemValidado);
        }
      } catch (error) {
        setMsgCep("Erro ao buscar o CEP. Tente novamente.");
        setCepIsValid(false);
        setVisualCepValidacao(estiloItemInvalidado);
      }
    }

    if (cep.length !== 8) {
      setMsgCep("Insira um cep válido");
      setCepIsValid(false);
      setVisualCepValidacao({});
    }
  };

  return (
    <section className={styles.cadastroSection}>
      <h1>CADASTRO</h1>

      <form action="post" onSubmit={handleCadastro}>
        <div>
          <label htmlFor="nome">Nome Completo</label>
          <input
            type="text"
            id="nome"
            value={nome}
            onChange={(e) => setNome(e.target.value)}
          />

          <label htmlFor="email">Email</label>
          <input
            type="email"
            id="email"
            value={email}
            onChange={handleEmail}
            style={visualEmailValidacao}
          />

          <label htmlFor="reEmail">Confirme seu Email</label>
          <input
            type="email"
            id="reEmail"
            value={confirmarEmail}
            onChange={handleConfirmarEmail}
            style={visualReEmailValidacao}
          />

          <label htmlFor="cep">CEP</label>
          <input
            type="text"
            name="cep"
            id="cep"
            value={cep}
            onChange={handleCep}
            style={visualCepValidacao}
          />
          <span>{msgCep}</span>
          <br />

          <label htmlFor="usernameCadastro">Username</label>
          <input
            type="text"
            id="usernameCadastro"
            value={usernameCadastro}
            onChange={handleUsername}
            style={visualUsernameValidacao}
          />

          <label htmlFor="passwordCadastro">Senha</label>
          <input
            type="password"
            id="passwordCadastro"
            value={passwordCadastro}
            onChange={(e) => setPasswordCadastro(e.target.value)}
          />

          <label htmlFor="rePassword">Confirme sua Senha</label>
          <input
            type="password"
            id="rePassword"
            value={confirmarSenhaCadastro}
            onChange={(e) => setConfirmarSenhaCadastro(e.target.value)}
          />

          <input type="submit" value="Cadastrar" />
          <span>{msgConfirmacao}</span>
        </div>
      </form>
    </section>
  );
};
