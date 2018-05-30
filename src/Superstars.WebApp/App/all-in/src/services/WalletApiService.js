import { postAsync } from "../helpers/apiHelper";
const endpoint = "/api/wallet";

class WalletApiService {
    constructor() {
    }

    async AddCoins(pseudo, coins, moneyType) {
        return await postAsync(`${endpoint}/${pseudo}/${coins}/${moneyType}/AddCoins`);
    }
}

export default new WalletApiService();