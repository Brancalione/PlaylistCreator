import React, { useState } from 'react';
import axios from 'axios';

function Principal() {
  const [Resposta1, setResposta1] = useState(0);
  const [Resposta2, setResposta2] = useState(0);
  const [Resposta3, setResposta3] = useState(0);
  const [Resposta4, setResposta4] = useState('');
  const [Resposta5, setResposta5] = useState('');
  const [selectedButtonFazendo, setSelectedButtonFazendo] = useState(null); // Botões do grupo "fazendo"
  const [selectedButtonMatchClash, setSelectedButtonMatchClash] = useState(null); // Botões do grupo "matchClash"
  const [Carregando, setCarregando] = useState(false);
  const [ExistPlaylist, setExistPlaylist] = useState('Criar Playlist');
  const [RespostaBackend, setRespostaBackend] = useState(null);

  const handleButtonClickFazendo = (resposta) => {
    setResposta4(resposta);
    setSelectedButtonFazendo(resposta); // Define o botão selecionado
  };

  const handleButtonClickMatchClash = (resposta) => {
    setResposta5(resposta);
    setSelectedButtonMatchClash(resposta); // Define o botão selecionado
  };

  const handleSubmit = async () => {
    const requestData = { Resposta1, Resposta2, Resposta3, Resposta4, Resposta5 };
    setCarregando(true);

    try {
      const response = await axios.post('https://localhost:7133/api/SolicitacaoFront', requestData);
      setRespostaBackend(response.data.url); 
      setExistPlaylist("Criar outra");
      alert("Dados enviados com sucesso!");
    } catch (error) {
      alert("Erro ao enviar dados");
      console.error("Erro:", error);
    }
    setCarregando(false);
  };

  return (
    <div className="container">
      <div className="sentindo">
        <p>Como você está se sentindo?</p>
        <p>Triste - Feliz</p>
        <input className='slider1' type="range" min="0" max="100" value={Resposta1} onChange={(e) => setResposta1(Number(e.target.value))} />
        <p>Calmo - Agitado</p>
        <input className="slider2" type="range" min="0" max="100" value={Resposta2} onChange={(e) => setResposta2(Number(e.target.value))} />
        <p>Estressado - Relaxado</p>
        <input className="slider3" type="range" min="0" max="100" value={Resposta3} onChange={(e) => setResposta3(Number(e.target.value))} />
      </div>

      <div className="fazendo">
        <p>O que você está fazendo?</p>
        <button 
          className={selectedButtonFazendo === 'Relaxando' ? 'selected' : ''} 
          onClick={() => handleButtonClickFazendo('Relaxando')}
        >Relaxando</button>
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
        <button 
          className={selectedButtonFazendo === 'Comendo' ? 'selected' : ''} 
          onClick={() => handleButtonClickFazendo('Comendo')}
        >Comendo</button>
        <button 
          className={selectedButtonFazendo === 'Treinando' ? 'selected' : ''} 
          onClick={() => handleButtonClickFazendo('Treinando')}
        >Treinando</button>
        <button 
          className={selectedButtonFazendo === 'Fumando' ? 'selected' : ''} 
          onClick={() => handleButtonClickFazendo('Fumando')}
        >Fumando</button>
      </div>

      <div className="matchClash">
        <p>Uma playlist que?</p>
        <button 
          className={selectedButtonMatchClash === 'Match' ? 'selected' : ''} 
          onClick={() => handleButtonClickMatchClash('Match')}
        >Match</button>
        <button 
          className={selectedButtonMatchClash === 'Clash' ? 'selected' : ''} 
          onClick={() => handleButtonClickMatchClash('Clash')}
        >Clash</button>
      </div>

      <div className="loginSpotify">
        <a href="https://accounts.spotify.com/authorize?client_id=4935730c846c489fbcfffdfbae4b19e4&response_type=code&redirect_uri=https://localhost:7133/api/SpotifyCallback/callback&scope=playlist-modify-public"
          target="_blank" rel="noopener noreferrer">Faça login no Spotify</a>
      </div>

      <div className="playlistButton">
        {Carregando ? (
          <p>Carregando...</p>
        ) : ( 
          <button onClick={handleSubmit}>{ExistPlaylist}</button>
        )}
      </div>

      {RespostaBackend && (
        <div>
          <p>Link da Playlist:</p>
          <a href={RespostaBackend} target="_blank" rel="noopener noreferrer">
            {RespostaBackend}
          </a>
        </div>
      )}
    </div>
  );
}

export default Principal;
