import { postAsync, deleteAsync, getAsync } from "../helpers/apiHelper";
const endpoint = "/api/game";

 class GameApiService {
   constructor() {
}

async createGame(gametype) {
    return await postAsync(`${endpoint}/${gametype}`);
}

async getYamsPot() {
    return await getAsync(`${endpoint}/getYamsPot`);
}

async DeleteAis() {
    return await deleteAsync(`${endpoint}/DeleteAis`);
}

async createAiUser() {
    return await postAsync(`${endpoint}/createAiUser`);
}

async UpdateStats(gametype, win) {
    return await postAsync(`${endpoint}/${gametype}/UpdateStats`, win);
}

async BetBTC(bet) {
    return await postAsync(`${endpoint}/${bet}/betBTC`);
}

async Bet(bet) {
    return await postAsync(`${endpoint}/${bet}/betYams`);
}

async BetBlackJack(bet) {
    return await postAsync(`${endpoint}/${bet}/betBlackJack`);
}

async getWinsBlackJackPlayer() {
    return await getAsync(`${endpoint}/getwinsBlackJackPlayer`);
}

async getLossesBlackJackPlayer() {
    return await getAsync(`${endpoint}/getlossesBlackJackPlayer`);
}

async getWinsYamsPlayer() {
    return await getAsync(`${endpoint}/getwinsYamsPlayer`);
}

async getLossesYamsPlayer() {
    return await getAsync(`${endpoint}/getlossesYamsPlayer`);
}

async getTrueProfitPlayer() {
    return await getAsync(`${endpoint}/gettrueprofitplayer`);
}

async getFakeProfitPlayer() {
    return await getAsync(`${endpoint}/getfakeprofitplayer`);
}

}

export default new GameApiService();