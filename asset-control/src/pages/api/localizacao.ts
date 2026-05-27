import { api } from "./api";

export async function buscarLocalizacao(){

    try{
        const response = await api.get("Localizacao")
        console.log("Deu certo o " + response)
        return response
    }
    catch(error: any)
    {
        throw new Error(error)
    }
}