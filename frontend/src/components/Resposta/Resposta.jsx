import styles from "./styles.module.scss";

export const Resposta = (props) => {
  return (
    <div className={styles.container}>
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
            Lorem ipsum dolor sit amet consectetur adipisicing elit. Distinctio
            optio necessitatibus exercitationem totam possimus labore illo
            accusamus maiores. Vitae officiis tenetur debitis distinctio
            corrupti dignissimos doloremque consequatur tempora temporibus
            neque!
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
  );
};
