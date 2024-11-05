import styles from "./styles.module.scss";
import { SlArrowDown, SlArrowUp } from "react-icons/sl";
import { useEffect, useState } from "react";

export const GraficoChamados = (props) => {
  const [isContentVisible, setIsContentVisible] = useState(false);

  const toggleContentVisibility = () => {
    setIsContentVisible(!isContentVisible); // Alterna visibilidade
  };

  return (
    <div className={styles.container}>
      <div className={styles.title} onClick={toggleContentVisibility}>
        <h1>Gráfico - Chamados</h1>
        {isContentVisible ? <SlArrowUp /> : <SlArrowDown />}
      </div>
    </div>
  );
};
