import React, { useState } from 'react';
import axios from 'axios';

function Principal() {
  const [Resposta1, setResposta1] = useState(3);
  const [Resposta2, setResposta2] = useState(3);
  const [Resposta3, setResposta3] = useState(3);
  const [Resposta4, setResposta4] = useState(null);
  const [Resposta5, setResposta5] = useState(null);
  const [selectedButtonFazendo, setSelectedButtonFazendo] = useState(null); // Botões do grupo "fazendo"
  const [selectedButtonMatchClash, setSelectedButtonMatchClash] = useState(null); // Botões do grupo "matchClash"
  const [Carregando, setCarregando] = useState(false);
  const [FezLogin, setFezLogin] = useState(false);
  const [ExistPlaylist, setExistPlaylist] = useState(null);
  const [RespostaBackend, setRespostaBackend] = useState(null);
  const [LinkPlaylist, setLinkPlaylist] = useState(null);
  const [LinkPlaylistPrevia, setLinkPlaylistPrevia] = useState(null);


  const handleButtonClickFazendo = (resposta) => {
    setResposta4(resposta);
    setSelectedButtonFazendo(resposta); // Define o botão selecionado
  };

  const handleButtonClickMatchClash = (resposta) => {
    setResposta5(resposta);
    setSelectedButtonMatchClash(resposta); 
  };

  const handleButtonClickLogin = () =>{
    setFezLogin(true)
    setExistPlaylist("Criar Playlist")
  };

  const handleSubmit = async () => {
    const requestData = { Resposta1, Resposta2, Resposta3, Resposta4, Resposta5 };
    setCarregando(true);

    try {
      const response = await axios.post('https://localhost:7133/api/SolicitacaoFront', requestData);
      setRespostaBackend(response.data.url); 
      setLinkPlaylist( "https://open.spotify.com/playlist/" + response.data.url )
      setLinkPlaylistPrevia("https://open.spotify.com/embed/playlist/" + response.data.url + "?utm_source=generator&theme=0" )
      setExistPlaylist("Não gostei!!! Tentar novamente");
      setCarregando(false);
    } catch (error) {
      alert("Erro ao enviar dados");
      console.error("Erro:", error);
    }
    setCarregando(false);
  };

  return (
    <div className="container">

      <div className='LogoEmacht'>
        <img className='logo' src="src\img\Spotify_Primary_Logo_RGB_Green.png" alt="logoSpotify" />

        <div className='descricaoApp'>
          <p className='titulo2'> Expresse o que está sentindo e veja</p>
          <p className='titulo3' >&nbsp; a playlist que criamos para você!</p>

          <div className='MatchClash'>
            <p className='titulo'>Quero uma playlist: </p>
            <button 
              className={selectedButtonMatchClash === 'Match' ? 'selected' : ''} 
              onClick={() => handleButtonClickMatchClash('Match')}
              >Match
            </button>
            <button 
              className={selectedButtonMatchClash === 'Clash' ? 'selected' : ''} 
              onClick={() => handleButtonClickMatchClash('Clash')}
              >Clash
            </button>
          </div>  
        </div>
      </div> 

      <div className='sentindoFazendo'>
        <div className="sentindo">
          <p className='titulo'>Como você está se sentindo?</p>
          <p>Feliz</p>
          <input className='slider1' type="range" min="0" max="100" value={Resposta1} onChange={(e) => setResposta1(Number(e.target.value))} />
          <p>Entusiasmado</p>
          <input className="slider2" type="range" min="0" max="100" value={Resposta2} onChange={(e) => setResposta2(Number(e.target.value))} />
          <p>Relaxado</p>
          <input className="slider3" type="range" min="0" max="100" value={Resposta3} onChange={(e) => setResposta3(Number(e.target.value))} />
        </div>

        <div className="fazendo">
          <p className='titulo'>O que você está fazendo nesse momento?</p>
          <div className='botoesFazendo'>
            <div className="botoesLinha1">
              <button 
                className={selectedButtonFazendo === 'Estudando' ? 'selected' : ''} 
                onClick={() => handleButtonClickFazendo('Estudando')}
              >Estudando</button>
              <button 
                className={selectedButtonFazendo === 'Trabalhando' ? 'selected' : ''} 
                onClick={() => handleButtonClickFazendo('Trabalhando')}
              >Trabalhando</button>
              <button 
                className={selectedButtonFazendo === 'Dirigindo' ? 'selected' : ''} 
                onClick={() => handleButtonClickFazendo('Dirigindo')}
              >Dirigindo</button>

            </div>

            <div className='botoesLinha2'>
            <button 
                className={selectedButtonFazendo === 'Comendo' ? 'selected' : ''} 
                onClick={() => handleButtonClickFazendo('Comendo')}
              >Comendo</button>
              <button 
                className={selectedButtonFazendo === 'Relaxando' ? 'selected' : ''} 
                onClick={() => handleButtonClickFazendo('Relaxando')}
              >Relaxando</button> 
              <button 
                className={selectedButtonFazendo === 'Treinando' ? 'selected' : ''} 
                onClick={() => handleButtonClickFazendo('Treinando')}
              >Treinando</button>
            </div>  
          </div>  
        </div>
      </div>

      <div className="loginSpotify">
        {!FezLogin && 
          (
            <a href="https://accounts.spotify.com/authorize?client_id=4935730c846c489fbcfffdfbae4b19e4&response_type=code&redirect_uri=https://localhost:7133/api/SpotifyCallback/callback&scope=playlist-modify-public"
            target="_blank" ><button onClick={handleButtonClickLogin}>Login Spotify</button></a>
          )
        }
      </div>
      
      <div className="criarPlaylist">
        {FezLogin && !Carregando && !RespostaBackend && 
        Resposta1 && Resposta2 && Resposta3 &&  
        Resposta4 && Resposta5 &&
          (
            <button onClick={handleSubmit}>Aqui</button>
          ) 
        }
      </div>

      <div className='carregando'>
        {Carregando &&
          (
            <p>Carregando...</p>
          ) 
        }
      </div>

      <div className='miniplaylist'>
        {RespostaBackend && !Carregando &&
          ( 
            <div > 
              <a className='titulo' 
                 href={LinkPlaylist} 
                 target="_blank" 
                 rel="noopener noreferrer">
                 Abrir no Spotify
              </a>
            
              <div>
                <iframe 
                  src={LinkPlaylistPrevia}
                  frameBorder="0" 
                  allowfullscreen="" 
                  allow="autoplay; clipboard-write;  fullscreen; picture-in-picture" 
                  loading="lazy"
                  height="180">  
                </iframe> 
              </div>

            </div>
          )
        } 
      </div>
      <div className='naoGostei'>
        {FezLogin && !Carregando && RespostaBackend &&
          (
            <button onClick={handleSubmit}>{ExistPlaylist}</button>
            ) 
        }
      </div>
    </div>
  );
}
export default Principal;
