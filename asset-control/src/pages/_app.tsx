import "@/src/styles/globals.css";
import Head from "next/head";
import Image from "next/image";
import { Montserrat } from "next/font/google";
import styles from "@/styles/Home.module.css";
import type { AppProps } from "next/app";
import { ToastContainer } from "react-toastify";

const MontserratT = Montserrat({
  variable: "--montserrat-texto",
  subsets: ["latin"],
  weight: ["100", "200", "300", "400", "500", "600", "700", "800", "900"]
})



export default function App({ Component, pageProps }: AppProps) {
  return (
  <main className={`${MontserratT.variable}`}>
  <Component {...pageProps} />
  <ToastContainer/>
  </main>
)}


