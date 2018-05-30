<template>
    <div id="wallet">
        <div style="text-align: center; margin-top: 2%;">
            <div class="btn-group">
                <button type="button" v-on:click="ChangeWallet('real')" class="btn btn-secondary">Real wallet</button>
            </div>
            <div class="btn-group">
                <button type="button" v-on:click="ChangeWallet('fake')" class="btn btn-secondary">Fake wallet</button>
            </div>
        </div>

        <!-- REALLY WALLET HERE -->
        <div style="text-align: center;" v-if="this.wallet == 'real'">
            <h1>Solde du compte réel: </h1>
        </div>

        <!-- FAKE WALLET HERE -->
        <div style="text-align: center;" v-if="this.wallet == 'fake'">
            <h1>Solde du compte virtuel: </h1>
        </div>

    </div>
</template>

<style lang="css">
  
</style>

<script>
import { mapActions } from 'vuex';
import WalletApiService from '../services/WalletApiService';
import UserApiService from '../services/UserApiService';

export default {
    data(){
        return {
            wallet: '',
            AllIn: 0,
        };
    },

    async mounted() {
        await this.AddCoins();
    },

    methods: {
        ...mapActions(['executeAsyncRequest']),
        async AddCoins() {
            await this.executeAsyncRequest(() => WalletApiService.AddCoins(UserApiService.pseudo, 1, 2));
        },

        ChangeWallet(wallet){
            this.wallet = wallet;
        }
    }
}
</script>

