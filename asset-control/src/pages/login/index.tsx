import styles from "./login.module.css";

import { Router, useRouter } from "next/router";
import { toast, ToastContainer } from "react-toastify";
import { useState } from "react";
import { login } from "../api/autenticacaoService";
import { erro, notificacao } from "@/src/utils/toast";

const Login = () => {
  const [nif, setNIF] = useState<string>("");
  const [senha, setSenha] = useState<string>("");

  const notificacao = (msg: string) => toast.success(msg);

  async function autenticar(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();
    try {
      await login(nif, senha);
      notificacao("Login bem sucedido");
    } catch (error: any) {
      erro(error.message);
    }
  }

  return (
    <>
      <main className={styles.loginPage}>
        <section
          className={styles.loginBanner}
          aria-label="Apresentação do sistema"
        >
          <img
            src="../imgs/Imagem do login.png"
            alt="Imagem de fundo relacionada à tecnologia"
            className={styles.bannerImage}
          />

          <div className={styles.bannerOverlay} />

          <div className={styles.bannerContent}>
            <img
              src="../imgs/Logo Senai.png"
              alt="Logo do SENAI"
              className={styles.senaiLogo}
            />

            <h2>Gestão de patrimônios</h2>

            <p className={styles.bannerContentText}>
              Controle, organização e transparência do patrimônio com
              eficiência.
            </p>

            <p />
          </div>
        </section>

        <section className={styles.loginArea} aria-label="Formulário de login">
          <form className={styles.loginForm} onSubmit={autenticar}>
            <h1>Login</h1>

            <div className={styles.formGroup}>
              <label htmlFor="nif">NIF:</label>

              <input
                type="text"
                id={styles.nif}
                name="nif"
                placeholder="Insira o seu NIF"
                value={nif}
                onChange={(e) => setNIF(e.target.value)}
              />
            </div>

            <div className={styles.formGroup}>
              <label htmlFor="senha">Senha:</label>

              <div className={styles.passwordField}>
                <input
                  type="password"
                  id={styles.senha}
                  name="senha"
                  placeholder="Insira a sua senha"
                  required
                  value={senha}
                  onChange={(e) => setSenha(e.target.value)}
                />

                <button
                  type="button"
                  className={styles.showPassword}
                  aria-label="Mostrar senha"
                >
                  👁
                </button>
              </div>
            </div>

            <button type="submit" className={styles.loginButton}>
              Entrar
            </button>
          </form>
        </section>
      </main>
    </>
  );
};

export default Login;
