import { toFormData } from "axios";
import { api } from "./api";

type patrimonioListar ={
    patrimonioID: string,
    denominacao: string,
    numeroPatrimonio: string,
    valor: string,
    imagem: string,
    localizacaoID: string
    statusPatrimonioID: string
}

type patrimonioPost = {
    patrimonioID: string,
    denominacao: string,
    numeroPatrimonio: string,
    valor: string,
    imagem: string,
    localizacaoID: string
    statusPatrimonioID: string
}

export class patrimonio_TSX {
    static toFormData(dados: patrimonioListar): FormData{
        const formData = new FormData

        formData.append("patrimonioID", dados.patrimonioID)
        formData.append("denominacao", dados.denominacao)
        formData.append("numeroPatrimonio", dados.numeroPatrimonio)
        formData.append("valor", dados.valor)
        // if (dados.imagem) {
        //     formData.append("imagem", dados.imagem);
        // }
        formData.append("localizacaoID", dados.localizacaoID)
        formData.append("statusPatrimonioID", dados.statusPatrimonioID)

        return formData
    }


}
export async function listarPatrimonio ()
{
    try{
        const response = await api.get("Patrimonio")
        console.log(response.data)        
        return response.data
    }

    catch(error: any){
        throw new Error(error.response.data)
    }

}


//! Tentar fazer post com csv, pesquisar depois
export async function adicionarPatrimonio(dados: patrimonioPost){
    try{
        const formData = patrimonio_TSX.toFormData(dados)
        const response = await api.post("Patrimonio", dados)
        return response
    }

    catch(error: any)
    {
        throw new Error(error.response.data)
    }

}


export async function buscarPatrimonioId(Id: string){
    try{
    const response = await api.get("Patrimonio" + Id)
    const patrimonioLink = {
        ...response.data
    }
    return patrimonioLink
    }
    catch(error: any){
        throw new Error(error.response.data)
    }
}
