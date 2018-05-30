import { postAsync, getAsync, deleteAsync } from "../helpers/apiHelper";
const endpoint = "/api/yams";

class YamsApiService {
    constructor(){

    }

    async RollDices(pseudo, selectedDices) {
        return await postAsync(`${endpoint}/${pseudo}/Roll`, selectedDices);
    }

    async createAIYams(pseudo, dices) {
        console.log(dices);
        return await postAsync(`${endpoint}/${pseudo}/CreateAIYams`, dices); 
    }
    async createAiUser(pseudo) {
        return await postAsync(`${endpoint}/${pseudo}/createAiUser`);
    }

    async CreateYamsPlayer(pseudo) {
        return await postAsync(`${endpoint}/${pseudo}/createPlayer`);
    }

    async CreateYamsAiPlayer(pseudo) {
        return await postAsync(`${endpoint}/${pseudo}/createAI`);
    }

    async DeleteYamsAiPlayer(pseudo) 
    {
        return await deleteAsync(`${endpoint}/${pseudo}/deleteAI`);
    }

    async GetPlayerDices(pseudo){
        var dices = await getAsync(`${endpoint}/${pseudo}`) + '';
        var array = dices.split('');
        return array;
    }

    async GetTurn(pseudo){
        return await getAsync(`${endpoint}/${pseudo}/getTurn`);
    }
}
export default new YamsApiService();