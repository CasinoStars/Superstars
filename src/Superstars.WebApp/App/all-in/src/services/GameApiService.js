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

async Bet(TrueOrFakeCoins) {
    var bet = prompt("Please enter your bet:", "500");
    if(bet == null)
        return window.location.replace("/Home/play");
    while (bet <= 0 || bet == ''){
        bet = prompt("Please enter a correct bet !", "500");
        if(bet == null)
            return window.location.replace("/Home/play");
        else if (bet > 0 && bet != '') {
            window.alert("You have bet " + bet + ' ' + TrueOrFakeCoins);
            var result = await postAsync(`${endpoint}/${bet}/bet`);
        }
    }
}
/*async CreateYamsGame(pot) {
    return await postAsync(`${endpoint}/CreateYamsGame`, pot);
}*/
}

export default new GameApiService();