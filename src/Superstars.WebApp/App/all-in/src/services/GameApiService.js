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

async Bet(balance) {
    var bet = prompt("Montant du pari:", "500");
    var betNumber = parseInt(bet);
    if(bet == null)
        return window.location.replace("/Home/play");
    else if(balance < bet || !betNumber){
        bet = prompt("Une erreur s'est produite. Montant maximum possible: " + balance, "500");
        betNumber = parseInt(bet);
    }else if(bet > 0 && bet != ''){
        window.alert("Vous avez bien parié " + bet + " All'In Virtuel");
        return await postAsync(`${endpoint}/${bet}/bet`);
    }
    while (bet <= 0 || bet == '' || bet > balance || !betNumber){
        if(bet == null)
            return window.location.replace("/Home/play");
        bet = prompt("Une erreur s'est produite ! Montant disponible: " + balance, "500");
        betNumber = parseInt(bet);
    }
    window.alert("Vous avez bien parié " + bet + " All'In Virtuel");
    return await postAsync(`${endpoint}/${bet}/bet`);
}

async BetBlackJack(balance) {
    var bet = prompt("Montant du pari:", "500");
    var betNumber = parseInt(bet);
    if(bet == null)
        return window.location.replace("/Home/play");
    else if(balance < bet || !betNumber){
        bet = prompt("Une erreur s'est produite. Montant maximum possible: " + balance, "500");
        betNumber = parseInt(bet);
    }else if(bet > 0 && bet != ''){
        window.alert("Vous avez bien parié " + bet + " All'In Virtuel");
        return await postAsync(`${endpoint}/${bet}/betBlackJack`);
    }
    while (bet <= 0 || bet == '' || bet > balance || !betNumber){
        if(bet == null)
            return window.location.replace("/Home/play");
        bet = prompt("Une erreur s'est produite ! Montant disponible: " + balance, "500");
        betNumber = parseInt(bet);
    }
    window.alert("Vous avez bien parié " + bet + " All'In Virtuel");
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