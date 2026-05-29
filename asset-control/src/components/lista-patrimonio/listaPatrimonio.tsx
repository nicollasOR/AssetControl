import Link from "next/link";
import styles from "./listaPatrimonio.module.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faCircleInfo,
  faChevronDown,
  faUser,
  faBars,
  faSliders,
  faArrowRightArrowLeft,
} from "@fortawesome/free-solid-svg-icons";
import CardPatrimonio from "../patrimonio/patrimonio";
import { useEffect, useState } from "react";
import ReactPaginate from "react-paginate";
import { listarPatrimonio } from "@/src/pages/api/patrimonio";
import { erro } from "@/src/utils/toast";
import { verificarAutenticacao } from "@/src/utils/autenticacao";
import { useRouter } from "next/router";

type Patrimonio = {
    patrimonioId: string
    patrimonio: string,
    denominacao: string,
    tipo: string,
    dataTransferencia: string,
    coordenadorAutenticado: boolean
    onTransferir: boolean
};

const ListaPatrimonioC = () => {
  const router = useRouter()
  const id  = router.query.id
  let telaEditar = !!id

  const [patrimonio, setPatrimonio] = useState<Patrimonio[]>([]);
  const [pesquisa, setPesquisa] = useState("");
  const [estaAutenticado, setEstaAutenticado] = useState(false);
  const [ordem, setOrdem] = useState("todos");

  const [primeiroItem, setPrimeiroItem] = useState(0);
  const numItem = 100;
  const ultimoPatrimonio = primeiroItem + numItem;
  const patrimoniosAtuais = patrimonio.slice(primeiroItem, ultimoPatrimonio);
  const paginas = Math.ceil(patrimonio.length / numItem);
  console.log("segue a var abaixo:")
  console.log(patrimoniosAtuais)
  const alterarPagina = (event: any) => {
    const newOffSet = (event.selected * numItem) % patrimonio.length;

    setPrimeiroItem(newOffSet);
  };

  const patrimoniosFiltrados = patrimonio.filter((patrimonios) => patrimonios.denominacao.toLowerCase().includes(pesquisa.toLowerCase()))
  // })

  async function listar() {
    try {
      const lista = await listarPatrimonio();
      console.log(patrimoniosFiltrados)
      setPatrimonio(lista);
    } catch (error: any) {
      erro(error.message);
    }



  }
      useEffect(() => {
        if(!router.isReady)
          return;
        if(!verificarAutenticacao())
          {
          router.push("/login")
          return
        }
      setEstaAutenticado(verificarAutenticacao())
      // if()
      listar()
    }, [router.isReady])
  return (
    <>
      <section
        className={`${styles.page_header} layout_guide`}
        aria-labelledby="titulo-patrimonios"
      >
        <h1 id={styles.titulo_patrimonio}>Patrimônios: Sala 09/10</h1>

        <form className={styles.search_area} role="search">
          <label htmlFor="pesquisa-ambiente" className={styles.sr_only}>
            Pesquisar patrimônios
          </label>

          <input
            type="search"
            id="pesquisa-ambiente"
            name="pesquisaAmbiente"
            placeholder="Pesquise o ambiente"
            value={pesquisa}
            onChange={(e) => setPesquisa(e.target.value)}
          />

          <button
            type="button"
            className={styles.filter_button}
            aria-label="Filtrar patrimonios"
          >
            <FontAwesomeIcon icon={faSliders} />
          </button>
        </form>
      </section>

      <section
        className={`${styles.table_section} layout_guide`}
        aria-label="Lista de patrimonios"
      >
        <table className={styles.environment_table}>
          <thead>
            <tr>
              <th>Patrimônio</th>
              <th>Denominação</th>
              <th>Tipo</th>
              <th>Data transfêrencia</th>
              <th>Detalhes</th>
              <th>Transferir</th>
            </tr>
          </thead>

<tbody>
  {patrimoniosAtuais.length > 0 ? (
    patrimoniosAtuais.map((item) => (
      <CardPatrimonio
        key={item.patrimonioId}
        patrimonioId={item.patrimonioId}
        dataTransferencia={item.dataTransferencia}
        denominacao={item.denominacao}
        tipo={item.tipo}
        patrimonio={item.patrimonio}
        onTransferir={item.onTransferir}
        coordenadorAutenticado={item.coordenadorAutenticado}
      />
    ))
  ) : (
    <tr>
      <td>Nenhum patrimônio encontrado.</td>
    </tr>
  )}
</tbody>
        </table>
      </section>

      <nav className={styles.pagination} aria-label="Paginação">
        <ReactPaginate
          containerClassName={styles.pagination}
          breakLabel="..."
          previousLabel={"<"}
          nextLabel={">"}
          previousLinkClassName={styles.pagination_button}
          nextLinkClassName={styles.pagination_button}
          renderOnZeroPageCount={null}
          pageClassName={styles.pagination_button}
          activeClassName={styles.current}
          breakLinkClassName={styles.pagination_link}
          onPageChange={alterarPagina}
          pageRangeDisplayed={paginas}
          pageCount={paginas}
        />
      </nav>
    </>
  );
};

export default ListaPatrimonioC;
