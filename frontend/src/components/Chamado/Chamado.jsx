import { Resposta } from "../Resposta/Resposta";
import styles from "./styles.module.scss";

export const Chamado = (props) => {
  return (
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
          nisi accusantium doloremque quisquam laboriosam vero pariatur maiores
          dolore fuga. Animi reiciendis natus ipsum aperiam quidem voluptates
          dolor, facilis ex perspiciatis.
        </p>
      </div>
      <Resposta />
    </section>
  );
};
