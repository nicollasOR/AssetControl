import { useState } from "react";
import styles from "./usuario.module.css"

const Usuario = () => {

    return (
        <main className={styles.page_content}>
            <h1 className={styles.titulo_pagina}>Responsáveis</h1>

            <section className={`${styles.container_cadastro_area} layout_guide`}>
                <h2 className={styles.titulo_secao}>Cadastrar novo responsável:</h2>

                <form className={styles.form_cadastro_responsavel}>
                    <div className={styles.campo_cadastro_responsavel}>
                        <label className={styles.label_responsavel} htmlFor="nome">Nome:</label>
                        <input className={styles.input_responsavel} type="text" id="nome" placeholder="Insira o nome do responsável" />
                    </div>

                    <div className={styles.campo_cadastro_responsavel}>
                        <label className={styles.label_responsavel} htmlFor="nif">NIF:</label>
                        <input className={styles.input_responsavel} type="text" id="nif" placeholder="Insira o NIF" />
                    </div>

                    <div className={styles.campo_cadastro_responsavel}>
                        <label className={styles.label_responsavel} htmlFor="rg">RG:</label>
                        <input className={styles.input_responsavel} type="text" id="rg" placeholder="Insira o RG do responsável" />
                    </div>

                    <div className={styles.campo_cadastro_responsavel}>
                        <label className={styles.label_responsavel} htmlFor="cpf">CPF:</label>
                        <input className={styles.input_responsavel} type="text" id="cpf" placeholder="Insira o CPF do responsável" 
                        />
                    </div>

                    <div className={styles.campo_cadastro_responsavel}>
                        <label className={styles.label_responsavel} htmlFor="carteira_trabalho">Nº Carteira de trabalho:</label>
                        <input className={styles.input_responsavel} type="text" id="carteira_trabalho" placeholder="Insira o Nº da carteira de trabalho do responsável" />
                    </div>

                    <div className={styles.campo_cadastro_responsavel}>
                        <label className={styles.label_responsavel} htmlFor="email">Email:</label>
                        <input className={styles.input_responsavel} type="email" id="email" placeholder="Insira o e-mail do responsável" />
                    </div>

                    <div className={styles.campo_cadastro_responsavel}>
                        <label className={styles.label_responsavel} htmlFor="cep">CEP:</label>
                        <input className={styles.input_responsavel} type="text" id="cep" placeholder="Insira o CEP do responsável" 
                      />
                    </div>

                    <div className={styles.campo_cadastro_responsavel}>
                        <label className={styles.label_responsavel} htmlFor="logradouro">Logradouro:</label>
                        <input className={styles.input_responsavel} type="text" id="logradouro" placeholder="Insira o logradouro do responsável"
                         />
                    </div>

                    <div className={styles.campo_cadastro_responsavel}>
                        <label className={styles.label_responsavel} htmlFor="numero">Número:</label>
                        <input className={styles.input_responsavel} type="number" id="numero" placeholder="Insira o número" />
                    </div>

                    <div className={styles.campo_cadastro_responsavel}>
                        <label className={styles.label_responsavel} htmlFor="complemento">Complemento:</label>
                        <input className={styles.input_responsavel} type="text" id="complemento" placeholder="Insira o complemento" />
                    </div>

                    <div className={styles.campo_cadastro_responsavel}>
                        <label className={styles.label_responsavel} htmlFor="bairro">Bairro:</label>
                        <input className={styles.input_responsavel} type="text" id="bairro" placeholder="Insira o bairro" 
                        />
                    </div>

                    <div className={styles.campo_cadastro_responsavel}>
                        <label className={styles.label_responsavel} htmlFor="cidade">Cidade:</label>
                        <input className={styles.input_responsavel} type="text" id="cidade" placeholder="Insira a cidade"
                         />
                    </div>

                    <div className={styles.campo_cadastro_responsavel}>
                        <label className={styles.label_responsavel} htmlFor="estado">Estado:</label>
                        <input className={styles.input_responsavel} type="text" id="estado" placeholder="Insira o estado" 
                         />
                    </div>

                    <button className={styles.botao_salvar}>Salvar</button>
                </form>
            </section>

            <section className={`${styles.table_section} layout_guide`} aria-label="Lista de responsáveis">
                <form className={styles.search_area} role="search">
                    <label htmlFor="pesquisa_responsavel" className={styles.sr_only}>
                        Pesquise o responsável
                    </label>

                    <input
                        type="search"
                        id="pesquisa_responsavel"
                        name="pesquisaResponsavel"
                        placeholder="Pesquise o responsável"
                    />

                    <button type="button" className={styles.filter_button} aria-label="Filtrar responsáveis">
                        <i className="fa-solid fa-sliders"></i>
                    </button>
                </form>

                <table className={styles.environment_table}>
                    <thead>
                        <tr>
                            <th>Nome</th>
                            <th>NIF</th>
                            <th>Ações</th>
                        </tr>
                    </thead>

                    <tbody>
                        <tr>
                            <td>Késsia Milena</td>
                            <td>1234567</td>
                            <td>
                                <a href="#" aria-label="Editar responsável">
                                    <i className="fa-regular fa-pen-to-square icon"></i>
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

                <a href="#" className={`${styles.pagination_link} ${styles.current}`} aria-current="page">1</a>
                <a href="#" className={styles.pagination_link}>2</a>
                <a href="#" className={styles.pagination_link}>3</a>

                <button type="button" className={styles.pagination_button} aria-label="Próxima página">
                    ›
                </button>
            </nav>
        </main>
    )
}

export default Usuario;