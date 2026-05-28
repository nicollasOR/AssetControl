import styles from "./exportar.module.css";

type modalProps = {
  modal?: string;
};

const EX_Imp_Card = ({ modal }: modalProps) => {
  return (
    <>
      {/* MODAL TRANSFERIR */}
      {modal === "transferir"} && (
      <section className={styles.modal_overlay}>
        <article className={styles.modal_container} id="modalTransferir">
          <a href="#" className={styles.modal_close}>
            x
          </a>

          <h1 className={styles.modal_title}>Transferir os patrimônios</h1>

          <form className={styles.modal_form}>
            <div className={styles.modal_field}>
              <label htmlFor="ambienteTransferencia">Ambiente</label>

              <select id="ambienteTransferencia">
                <option>Manutenção</option>
                <option>Sala XX</option>
                <option>Sala XX</option>
              </select>
            </div>

            <div className={styles.modal_field}>
              <label htmlFor="motivoTransferencia">
                Motivo da transferência
              </label>

              <textarea id="motivoTransferencia" placeholder="Lorem"></textarea>
            </div>

            <button className={styles.modal_button}>TRANSFERIR</button>
          </form>
        </article>
      </section>
      ){modal === "exportar"} && (
      <section className={styles.modal_overlay}>
        <article className={styles.modal_container} id="modalImportar">
          <a href="#" className={styles.modal_close}>
            x
          </a>

          <h1 className={styles.modal_title}>Importar novos patrimônios</h1>

          <form className={styles.modal_form}>
            <div className={styles.modal_field}>
              <label htmlFor="numeroPatrimonio">Número do patrimônio</label>
              <input type="text" id="numeroPatrimonio" placeholder="1245769" />
            </div>

            <div className={styles.modal_field}>
              <label htmlFor="denominacaoPatrimonio">Denominação</label>
              <input
                type="text"
                id="denominacacaoPatrimonio"
                placeholder="CADEIRA GIRATORIA EM POLIPROPILENO"
              />
            </div>

            <button className={styles.modal_button}>SALVAR IMPORTAÇÃO</button>
          </form>
        </article>
      </section>
      ){/* MODAL JUSTIFICATIVA */}
      <section className={styles.modal_overlay}>
        <article
          className={`${styles.modal_container} ${styles.modal_justificativa}`}
        >
          <a href="#" className={styles.modal_close}>
            x
          </a>

          <h1 className={styles.modal_title}>Justificativa</h1>

          <p className={styles.modal_text}>
            Lorem ipsum dolor sit amet consectetur adipisicing elit. Veritatis,
            quasi distinctio! Temporibus similique expedita laboriosam,
            assumenda officia veritatis amet doloremque esse obcaecati
            repudiandae architecto in sed facilis quas harum.
          </p>
        </article>
      </section>
    </>
  );
};

export default EX_Imp_Card;
