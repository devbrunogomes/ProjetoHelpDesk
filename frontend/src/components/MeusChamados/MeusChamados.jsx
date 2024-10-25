import styles from "./styles.module.scss";

export const MeusChamados = (props) => {
  return (
    <div className={styles.container}>
      <div>
        <h1>MEUS CHAMADOS</h1>
      </div>
      <section>
        <div className={styles.titulo}>
          <h2>Chamado #01</h2>
          <h2>Em Andamento</h2>
          <h2>Baixa Prioridade</h2>
        </div>

        <div className={styles.subTitulo}>
          <span>27/10/2024</span>
          <span>Internet caindo toda hora</span>
        </div>
        <div className={styles.descricao}>
          <p>
            Lorem ipsum dolor sit, amet consectetur adipisicing elit. Deleniti
            nisi accusantium doloremque quisquam laboriosam vero pariatur
            maiores dolore fuga. Animi reiciendis natus ipsum aperiam quidem
            voluptates dolor, facilis ex perspiciatis.
          </p>
        </div>
        <div className={styles.respostasContainer}>
          <div>
            <h3>Respostas</h3>
          </div>
          <div className={styles.respostas}>
            <div>
              <span>28/10/2024 -</span>
              <span> Autor</span>
            </div>
            <div>
              <p>
                Lorem ipsum dolor sit amet consectetur adipisicing elit.
                Distinctio optio necessitatibus exercitationem totam possimus
                labore illo accusamus maiores. Vitae officiis tenetur debitis
                distinctio corrupti dignissimos doloremque consequatur tempora
                temporibus neque!
              </p>
            </div>
          </div>
          <div>
            <h3>Reponder</h3>
          </div>
          <form action="post">
            <textarea
              name="resposta"
              id="resposta"
              cols="50"
              rows="2"
              required
            ></textarea>
            <input type="submit" value="Enviar Resposta" />
          </form>
        </div>
      </section>
    </div>
  );
};
