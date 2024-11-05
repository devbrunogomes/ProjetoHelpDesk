import { useState } from "react";
import styles from "./styles.module.scss";
import axios from "axios";
import { SlArrowDown, SlArrowUp } from "react-icons/sl";
import { Tecnico } from "../Tecnico/Tecnico";
import { BsSearch } from "react-icons/bs";
import { Cliente } from "../Cliente/Cliente";

export const PesquisarCliente = (props) => {
  const [isFormVisible, setIsFormVisible] = useState(false);
  const [clienteId, setClienteId] = useState("");
  const [cliente, setCliente] = useState(null);

  const toggleFormVisibility = () => {
    setIsFormVisible(!isFormVisible);
  };

  async function handleSubmit(e) {
    e.preventDefault();

    setCliente(null);

    const token = localStorage.getItem("token");

    try {
      const response = await axios.get(
        `http://localhost:5089/Cliente/${clienteId}`,
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );

      console.log(response.data);
      setCliente(response.data);
    } catch (error) {
      setCliente(null);
      console.error(error);
    }
  }

  return (
    <div className={styles.container}>
      <div className={styles.title} onClick={toggleFormVisibility}>
        <h1>Pesquisar Cliente</h1>
        {isFormVisible ? <SlArrowUp /> : <SlArrowDown />}
      </div>
      {isFormVisible && (
        <>
          <form action="post" onSubmit={async (e) => await handleSubmit(e)}>
            <input
              placeholder="Id do Cliente"
              name="tecnicoId"
              type="text"
              value={clienteId}
              onChange={(e) => setClienteId(e.target.value)}
            />
            <button type="submit">
              <BsSearch />
            </button>
          </form>
          <div>{cliente && <Cliente cliente={cliente} />}</div>
        </>
      )}
    </div>
  );
};
