import { useState } from "react";
import styles from "./styles.module.scss";
import axios from "axios";
import * as validacao from "../../functions/ValidacaoDados";
import { SlArrowDown, SlArrowUp } from "react-icons/sl";

export const CadastroTecnicoAdm = (props) => {
  //Visibilidade
  const [isFormVisible, setIsFormVisible] = useState(false);

  //Tipo de Cadastro Variáveis
  const [tipoCadastro, setTipoCadastro] = useState("Técnico");

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
  const [cidade, setCidade] = useState("");
  const [estado, setEstado] = useState("");
  const [cepIsValid, setCepIsValid] = useState(false);

  //Msg de confirmação
  const [msgConfirmacao, setMsgConfirmacao] = useState("");

  const handleCadastro = async (event) => {
    event.preventDefault();

    if (!validacao.validarNomeCompleto(nome)) {
      alert("Nome Completo inválido");
      return;
    }

    if (!validacao.validarIgualdadeEmail(email, confirmarEmail)) {
      alert("Emails são diferentes");
      return;
    }

    if (!cepIsValid) {
      alert("CEP Inválido");
      return;
    }

    if (!validacao.validarUsername(usernameCadastro)) {
      alert("Username Inválido");
      return;
    }

    if (
      !validacao.validarIgualdadeSenhas(
        passwordCadastro,
        confirmarSenhaCadastro
      )
    ) {
      alert("Senhas são diferentes");
      return;
    }

    if (!validacao.validarSenha(passwordCadastro)) {
      alert(
        "Senha precisa ter:  entre 8 e 16 caracteres, incluindo letras e números"
      );
      return;
    }

    const token = localStorage.getItem("token");

    const tipoCadastroNormalizado = tipoCadastro
      .normalize("NFD")
      .replace(/[\u0300-\u036f]/g, "");

    const data = {
      nomeCompleto: nome,
      email: email,
      emailConfirmation: confirmarEmail,
      userName: usernameCadastro,
      password: passwordCadastro,
      rePassword: confirmarSenhaCadastro,
      cep: cep,
    };
    console.log(tipoCadastro);

    try {
      // Fazer a requisição POST para o backend
      const response = await axios.post(
        `http://localhost:5089/${tipoCadastroNormalizado}`,
        data,
        {
          headers: {
            Authorization: `Bearer ${token}`,
            "Content-Type": "application/json",
          },
        }
      );

      console.log(response);
      setNome("");
      setEmail("");
      setConfirmarEmail("");
      setCep("");
      setUsernameCadastro("");
      setPasswordCadastro("");
      setConfirmarSenhaCadastro("");
      setMsgConfirmacao(`${tipoCadastro} Cadastrado com Sucesso!`);
    } catch (error) {
      console.error(error);
      setMsgConfirmacao(error.response.data);
    }
  };

  const handleCep = async (event) => {
    const cep = event.target.value.replace(/\D/g, ""); // Remove caracteres não numéricos
    if (cep.length !== 8) {
      setMsgCep("CEP inválido. Deve conter 8 dígitos.");
      return;
    }
    try {
      const response = await fetch(`https://viacep.com.br/ws/${cep}/json/`);
      const data = await response.json();

      if (data.erro) {
        setMsgCep("CEP não encontrado.");
        setCepIsValid(false);
        event.target.focus();
      } else {
        setCidade(data.localidade);
        setEstado(data.uf);
        setMsgCep(`${data.localidade} - ${data.uf}`); // Usa os dados diretamente aqui
        setCepIsValid(true);
      }
    } catch (error) {
      setMsgCep("Erro ao buscar o CEP. Tente novamente.");
      setCepIsValid(false);
    }
  };

  const toggleFormVisibility = () => {
    setIsFormVisible(!isFormVisible); // Alterna visibilidade
  };

  return (
    <section className={styles.cadastroSection}>
      <div className={styles.title} onClick={toggleFormVisibility}>
        <h1>Novo Colaborador</h1>
        {isFormVisible ? <SlArrowUp /> : <SlArrowDown />}
      </div>
      {isFormVisible && (
        <form action="post" onSubmit={handleCadastro}>
          <div>
            <div className={styles.tipoWrapper}>
              <label htmlFor="tipoRegistro">Registrar Novo: </label>
              <select
                name="tipoRegistro"
                id="tipoRegistro"
                value={tipoCadastro}
                onChange={(e) => setTipoCadastro(e.target.value)}
              >
                <option value="Tecnico">Técnico</option>
                <option value="Administrador">Administrador</option>
              </select>
            </div>
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
              onChange={(e) => setEmail(e.target.value)}
            />

            <label htmlFor="reEmail">Confirme seu Email</label>
            <input
              type="email"
              id="reEmail"
              value={confirmarEmail}
              onChange={(e) => setConfirmarEmail(e.target.value)}
            />

            <label htmlFor="cep">CEP</label>
            <input
              type="text"
              name="cep"
              id="cep"
              value={cep}
              onChange={(e) => setCep(e.target.value)}
              onBlur={handleCep}
            />
            <span>{msgCep}</span>
            <br />

            <label htmlFor="usernameCadastro">Username</label>
            <input
              type="text"
              id="usernameCadastro"
              value={usernameCadastro}
              onChange={(e) => setUsernameCadastro(e.target.value)}
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
      )}
    </section>
  );
};