
import Header from '@/src/components/header/Header'
import styles from './detalhepatrimonio.module.css'
import { faCircleInfo, faChevronDown, faUser, faBars, faSliders, faArrowRightArrowLeft, faArrowLeft } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
const detalhePatrimonio = () => {
  return(
    <>
    <Header/>

<main className={`page_content`}>
    <section
        className={`${styles.page_detalhes} layout_guide`}
        aria-labelledby="titulo_patrimonio"
    >
        <a
            href="#"
            className={styles.back_link}
        >
            <i className="fa-solid fa-arrow-left"></i>
            <FontAwesomeIcon icon={faArrowLeft}/>
            Voltar
        </a>

        <h1 id="titulo_patrimonio">
            Patrimônio: 1236808
        </h1>

        <article className={styles.patrimonio_card}>
            <div className={styles.patrimonio_content}>
                <dl>
                    <dt>Denominação</dt>
                    <dd>
                        NOTEBOOK ALTO DESEMPENHO P/ GAMER
                    </dd>
                </dl>

                <dl>
                    <dt>Tipo</dt>
                    <dd>Mesa</dd>
                </dl>

                <dl>
                    <dt>Data transferência</dt>

                    <dd>
                        <time dateTime="2026-02-09">
                            09/02/2026
                        </time>
                    </dd>
                </dl>

                <dl>
                    <dt>Local Atual</dt>
                    <dd>Sala 09/10</dd>
                </dl>

                <dl>
                    <dt>Status Atual</dt>
                    <dd>Ativo</dd>
                </dl>
            </div>
        </article>
    </section>

    <section
        className={`${styles.table_section} ${styles.layout_guide}`}
        aria-label="Lista de histórico do patrimônio"
    >
        <h2>Histórico</h2>

        <table className={styles.history_table}>
            <thead>
                <tr>
                    <th>Data</th>
                    <th>Tipo de movimentação</th>
                    <th>Origem</th>
                    <th>Destino</th>
                    <th>Responsável</th>
                    <th>Justificativa</th>
                </tr>
            </thead>

            <tbody>
                <tr>
                    <td data-label="Data">
                        11/02/2026
                    </td>

                    <td data-label="Tipo de movimentação">
                        <span className={styles.status_badge}>
                            Transferência
                        </span>
                    </td>

                    <td data-label="Origem">
                        Sala 07/08
                    </td>

                    <td data-label="Destino">
                        Sala 09/10
                    </td>

                    <td data-label="Responsável">
                        Gustavo Lima
                    </td>

                    <td data-label="Justificativa">
                        <a
                            href="#"
                            aria-label="Ver justificativa da transferência"
                        >
                            <FontAwesomeIcon icon={faCircleInfo}/>
                        </a>
                    </td>
                </tr>
            </tbody>
        </table>
    </section>
</main>

    </>
  )
}

export default detalhePatrimonio