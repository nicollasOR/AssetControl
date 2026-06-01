// import Header from "@/components/header/header";
import Header from "@/src/components/header/Header";
import styles from "./patrimonio.module.css"
import { Info, Pencil, Upload, SlidersHorizontal } from "lucide-react";
import { useState } from "react";
import { importarPatrimonioCSV } from "../../api/patrimonio";


const Patrimonio = () => {

    const [arquivo, setArquivo] = useState<File | null>(null)
    return (
        <>
            <Header />
            <main className={styles.page_content}>
                <h1 className={styles.titulo_pagina}>Patrimônios</h1>

                <section className={`${styles.container_cadastro_area} layout_guide`} >
                    <h2 className={styles.titulo_secao}>Importar patrimônios</h2>

                    <form className={styles.form_cadastro_area}>
                        <div className={styles.campo_cadastro_area}>
                            <label className={styles.label_area} htmlFor="area">Upload de arquivo csv:</label>

                            <div className={styles.upload_box}>
                                <input className={styles.input_area} type="file" id="area" accept=".csv" />
                                <label className={styles.upload_label} htmlFor="area">
                                    {/* <i className="fa-solid fa-upload"></i> */}
                                    <Upload />
                                </label>
                            </div>
                        </div>

                        <button className={styles.botao_salvar}>Salvar</button>
                    </form>
                </section>
                <section className={`${styles.table_section} layout_guide`} aria-label="Lista de ambientes">
                    <form className={styles.search_area} role="search">
                        <label htmlFor={styles.pesquisa_ambiente} className={styles.sr_only}>Pesquise o patrimônio</label>

                        <input type="search" id="pesquisa_ambiente" name="pesquisaAmbiente" placeholder="Pesquise o ambiente" />

                        <button type="button" className={styles.filter_button} aria-label="Filtrar ambientes">
                            {/* <i className="fa-solid fa-sliders"></i> */}
                            <SlidersHorizontal />
                        </button>
                    </form>
                    <table className={styles.environment_table}>
                        <thead>
                            <tr>
                                <th>Patrimônio</th>
                                <th>Denominação</th>
                                <th>Data transferência</th>
                                <th>Ações</th>
                            </tr>
                        </thead>

                        <tbody>
                            <tr className="">
                                <td>123456</td>
                                <td>MESA TRAPEZOIDAL DC-1987</td>
                                <td>17/02/2022</td>
                                <td>
                                    <a href="#" aria-label="Ver detalhes do patrimônio">
                                        {/* <i className="fa-solid fa-circle-info icon"></i> */}
                                        <Info />
                                    </a>
                                    <a href="#" aria-label="Editar área">
                                        {/* <i className="fa-regular fa-pen-to-square icon"></i> */}
                                        <Pencil />
                                    </a>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </section>

                <nav className={styles.pagination} aria-label="Paginação">
                    <button type="button" className={styles.pagination_button} aria-label="Página anterior">
                        ‹
                    </button>

                    <a href="#" className={`${styles.pagination_link} ${styles.current} `} aria-current="page">1</a>
                    <a href="#" className={styles.pagination_link}>2</a>
                    <a href="#" className={styles.pagination_link}>3</a>

                    <button type="button" className={styles.pagination_button} aria-label="Próxima página">
                        ›
                    </button>
                </nav>
            </main>
        </>
    )
}

export default Patrimonio;