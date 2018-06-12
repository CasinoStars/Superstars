import { postAsync, getAsync, getAsyncNoJSON, deleteAsync } from "../helpers/apiHelper";
const endpoint = "/api/blackJack";

class BlackJackApiService {
    constructor() {
    }

    async CreateJackPlayer() {
        await postAsync(`${endpoint}/CreatePlayer`);
    }

    async CreateJackAiPlayer() {
        await postAsync(`${endpoint}/CreateAi`);
    }

    async DeleteJackAiPlayer() {
        await deleteAsync(`${endpoint}/DeleteAi`);
    }
    
    async InitPlayer() {
        await postAsync(`${endpoint}/InitPlayer`);
    }

    async InitIa() {
        await postAsync(`${endpoint}/InitAI`);
    }

    async GetPlayerCards() {
        var cards = await getAsyncNoJSON(`${endpoint}/GetPlayerCards`) + '';
        var array = cards.split(',');
        return array;
    }

    async GetAiCards() {
        var cards = await getAsyncNoJSON(`${endpoint}/GetAiCards`) + '';
        var array = cards.split(',');
        return array;
    }

    async HitPlayer() {
        await postAsync(`${endpoint}/HitPlayer`);
    }

    async GetTurn() {
        return await getAsync(`${endpoint}/getTurn`);
    }

    getplayerHandValue() {
        var value = getAsyncNoJSON(`${endpoint}/getplayerHandValue`);
        console.log("ici" + value)
        return value;
    }
}

export default new BlackJackApiService();