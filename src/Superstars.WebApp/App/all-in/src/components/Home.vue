<template>
    <div class="home">
        <header style="padding:3%;">
            <div style="text-align:center; letter-spacing: 1px; font-family: 'Courier New', sans-serif;">
                <h1>Bienvenue sur ALL`IN</h1>
            </div>
        </header>
        <div class="warning slide bg-danger" id="baniere" style="text-align:center; letter-spacing: 1px; font-family: 'Courier New', sans-serif;">
            <h4 style="padding-top:1.5%;">Attention, le casino est actuellement en développement et utilise la <a href="https://en.bitcoin.it/wiki/Testnet"
                    style="color:blue" target="_blank">blockchain test</a></h4>
        </div>

        <br>
        <div class="bs-example">
            <div id="myCarousel" class="carousel slide bg-dark" data-ride="carousel" data-interval=4000>
                <!-- Carousel indicators -->
                <ol class="carousel-indicators">
                    <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                    <li data-target="#myCarousel" data-slide-to="1"></li>
                    <li data-target="#myCarousel" data-slide-to="2"></li>
                    <li data-target="#myCarousel" data-slide-to="3"></li>
                    <li data-target="#myCarousel" data-slide-to="4"></li>
                </ol>
                <!-- Wrapper for carousel items -->
                <div class="carousel-inner" style="text-align: center; font-family: 'Courier New', sans-serif;">
                    <div class="carousel-item active">
                        <img src="../img/../img/provablylogo.png" id="provablylogo">
                        <div class="carousel-caption d-md-block">
                            <h5><strong>Un site provably fair</strong><br>Vous pouvez parier les yeux fermés</h5>
                        </div>
                    </div>
                    <div class="carousel-item">
                        <img src="../img/fastbetlogo.png" id="fastbetlogo">
                        <div class="carousel-caption d-md-block">
                            <h5><strong>Des jeux rapides</strong><br>Mais attention à ne pas vous brûler</h5>
                        </div>
                    </div>
                    <div class="carousel-item">
                        <img src="../img/statslogo.png" id="statslogo">
                        <div class="carousel-caption d-md-block">
                            <h5><strong>Des statistiques détaillées</strong><br>Visualisez vos statistiques en temps
                                réel</h5>
                        </div>
                    </div>
                    <div class="carousel-item">
                        <img src="../img/bitcoinlogo.png" id="bitcoinlogo">
                        <div class="carousel-caption d-md-block">
                            <h5><strong>Un Bitcoin Casino</strong><br>La maison accepte le Bitcoin</h5>
                        </div>
                    </div>
                    <div class="carousel-item">
                        <img src="../img/leaderboardlogo.png" id="leaderboardlogo">
                        <div class="carousel-caption d-md-block">
                            <h5><strong>Classement général</strong><br>Comparez votre parcours par rapport à vos amis</h5>
                        </div>
                    </div>
                </div>
                <!-- Carousel controls -->
                <a class="carousel-control-prev left" href="#myCarousel" data-slide="prev">
                    <span class="carousel-control-prev-icon"></span>
                    <span class="sr-only">Previous</span>
                </a>
                <a class="carousel-control-next right" href="#myCarousel" data-slide="next">
                    <span class="carousel-control-next-icon"></span>
                    <span class="sr-only">Next</span>
                </a>
            </div>
        </div>

        <center style="padding-top: 4%;">
            <strong><a class="bankRoll">- BANQUE DU SITE -<br>{{BTCBankCoins.toLocaleString('en')}}<i class="fa fa-btc"></i>
                    | {{fakeBankCoins.toLocaleString('en')}}<i class="fa fa-money"></i> </a></strong>
            <br><br><br><br>
            <strong><a class="bankRoll">- LA COMMUNAUTÉ A DÉJA PARLÉ -<br>{{numberPlayers.toLocaleString('en')}}
                    JOUEURS <br>{{totalWageredBTC.toLocaleString('en')}}<i class="fa fa-btc"></i> |
                    {{totalWageredFake.toLocaleString('en')}}<i class="fa fa-money"></i></a></strong>
        </center>

        <!-- Footer -->
        <br><br>
        <footer class="page-footer font-small blue">

            <!-- Copyright -->
            <div class="footer-copyright text-center py-3">Jouer comporte des risques : endettement, isolement,
                dépendance... -
                <a href="http://www.joueurs-info-service.fr/"> Besoin d'aide ? </a> - 09 74 75 13 13 (FRA)
            </div>
            <!-- Copyright -->

        </footer>
        <!-- Footer -->
    </div>
</template>

<script>
    import {
        mapActions
    } from 'vuex';
    import Vue from 'vue';
    import WalletApiService from '../services/WalletApiService';
    import BackOfficeApiService from '../services/BackOfficeApiService';
    import RankApiService from '../services/RankApiService';


    export default {

        data() {
            return {
                BTCBankCoins: 0,
                fakeBankCoins: 0,
                totalWageredBTC: 0,
                totalWageredFake: 0,
                numberPlayers: 0

            }
        },

        async created() {
            await this.BTCBank();
            await this.fakeBank();
            await this.Wagered();
            await this.GetNumberOfPlayers();
        },

        async mounted() {},

        methods: {
            ...mapActions(['executeAsyncRequest']),

            async BTCBank() {
                this.BTCBankCoins = await this.executeAsyncRequest(() => WalletApiService.GetBTCBankRoll());
            },

            async Wagered() {
                this.totalWageredBTC = await this.executeAsyncRequest(() => RankApiService.GetTotalMoneyWagered(1));
                this.totalWageredFake = await this.executeAsyncRequest(() => RankApiService.GetTotalMoneyWagered(0));
            },

            async GetNumberOfPlayers() {
                this.numberPlayers = await this.executeAsyncRequest(() => RankApiService.GetNumberOfPlayers());
            },

            async fakeBank() {
                this.fakeBankCoins = await this.executeAsyncRequest(() => WalletApiService.GetFakeBankRoll());
            },
        }
    }
</script>

<style lang="css">
    @media(max-width: 1109px) {
        .home #baniere>h4 {
            font-size: inherit;
        }
    }

    @media(max-width: 562px) {
        .home .bankRoll {
            font-size: 3.8vh;
            font-family: 'Courier New', sans-serif;
            font-variant: small-caps;
        }

        .home h5 {
            color: white;
            text-align: center;
            font-family: 'Courier New', sans-serif;
            font-variant: small-caps;
            font-size: 3.8vh;
        }

        .home h1 {
            font-size: 3vh;
        }
    }

    @media(min-width: 562px) {
        .home .bankRoll {
            font-size: 35px;
            font-family: 'Courier New', sans-serif;
            font-variant: small-caps;
        }

        .home h5 {
            color: white;
            text-align: center;
            font-family: 'Courier New', sans-serif;
            font-variant: small-caps;
            font-size: 30px;
        }
    }

    .home .carousel {
        /* background: #212529; */
    }

    .home .carousel .carousel-item {
        height: 450px;
        min-height: 350px;
        /* Prevent carousel from being distorted if for some reason image doesn't load */
    }


    .home .warning {
        height: 40px;
        min-height: 70px;
        /* Prevent carousel from being distorted if for some reason image doesn't load */
    }


    .home .carousel .carousel-item img {
        margin-top: 5%;
        /* Align slide image horizontally center */
    }

    #provablylogo {
        margin-top: 3.5%;
        width: 180px;
        height: 230px;
    }

    #statslogo {
        margin-left: 3%;
        width: 260px;
        height: 170px;
    }

    #fastbetlogo {
        margin-top: 4%;
        width: 180px;
        height: 190px;
    }

    #bitcoinlogo {
        width: 180px;
        height: 170px;
    }

    #leaderboardlogo {
        width: 150px;
        height: 190px;
    }
</style>