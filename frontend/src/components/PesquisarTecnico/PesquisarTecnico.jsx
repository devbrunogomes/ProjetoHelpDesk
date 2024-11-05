import { useState } from "react";
import styles from "./styles.module.scss";
import axios from "axios";
import { SlArrowDown, SlArrowUp } from "react-icons/sl";
import { Tecnico } from "../Tecnico/Tecnico";
import { BsSearch } from "react-icons/bs";

export const PesquisarTecnicos = (props) => {
  const [isFormVisible, setIsFormVisible] = useState(false);
  const [tecnicoId, setTecnicoId] = useState("");
  const [tecnico, setTecnico] = useState(null);

  const toggleFormVisibility = () => {
    setIsFormVisible(!isFormVisible);
  };

  async function handleSubmit(e) {
    e.preventDefault();

    setTecnico(null);

    const token = localStorage.getItem("token");

    try {
      const response = await axios.get(
        `http://localhost:5089/Tecnico/${tecnicoId}`,
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );

      console.log(response.data);
      setTecnico(response.data);
    } catch (error) {
      setTecnico(null);
      console.error(error);
    }
  }

  return (
    <div className={styles.container}>
      <div className={styles.title} onClick={toggleFormVisibility}>
        <h1>Pesquisar Técnico</h1>
        {isFormVisible ? <SlArrowUp /> : <SlArrowDown />}
      </div>
      {isFormVisible && (
        <>
          <form action="post" onSubmit={async (e) => await handleSubmit(e)}>            
            <input
            placeholder="Id do Técnico"
              name="tecnicoId"
              type="text"
              value={tecnicoId}
              onChange={(e) => setTecnicoId(e.target.value)}
            />
            <button type="submit">
              <BsSearch />
            </button>
          </form>
          <div>{tecnico && <Tecnico tecnico={tecnico} />}</div>
        </>
      )}
      {isFormVisible && <div></div>}
    </div>
  );
};
