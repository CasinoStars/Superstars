import { postAsync, getAsync, deleteAsync } from "../helpers/apiHelper";
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

    async GetPlayerCards() {
        var cards = await getAsync(`${endpoint}/GetPlayerCards`) + '';
        console.log(cards);
        return cards;
    }

    async GetTurn() {
        return await getAsync(`${endpoint}/getTurn`);
    }
}

export default new BlackJackApiService();