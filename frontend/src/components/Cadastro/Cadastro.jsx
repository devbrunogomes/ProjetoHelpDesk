import styles from "./styles.module.scss";

export const Cadastro = () => {
  return (
    <main className={styles.container}>
      <section className={styles.cadastroSection}>
        <h1>Cadastro</h1>

        <form action="post">
          <label htmlFor="nome">Nome Completo</label>
          <input type="text" id="nome" />

          <label htmlFor="email">Email</label>
          <input type="email" id="email" />

          <label htmlFor="reEmail">Confirme seu Email</label>
          <input type="email" id="reEmail" />

          <label htmlFor="cep">CEP</label>
          <input type="text" name="cep" id="cep" />

          <label htmlFor="usernameCadastro">Username</label>
          <input type="text" id="usernameCadastro" />

          <label htmlFor="passwordCadastro">Senha</label>
          <input type="password" id="passwordCadastro" />

          <label htmlFor="rePassword">Confirme sua Senha</label>
          <input type="password" id="rePassword" />

          <input type="submit" value="Cadastrar" />
        </form>
      </section>

      <section className={styles.loginSection}>
        <h1>LOGIN</h1>
        <form action="post">
          <label htmlFor="usernameLogin">Username</label>
          <input type="text" id="usernameLogin" />
          <label htmlFor="passwordLogin">Password</label>
          <input type="password" name="" id="passwordLogin" />
          <input type="submit" value="Login" />
        </form>
      </section>
    </main>
  );
};
