import React, { useState } from 'react';
import axios from 'axios';

function Principal() {
  const [Resposta1, setResposta1] = useState(0);
  const [Resposta2, setResposta2] = useState(0);
  const [Resposta3, setResposta3] = useState(0);
  const [Resposta4, setResposta4] = useState('');
  const [Resposta5, setResposta5] = useState('');
  const [Carregando, setCarregando] = useState(false);
  const [ExistPlaylist, setExistPlaylist] = useState('Criar Playlist');
  const [RespostaBackend, setRespostaBackend] = useState(null);

  const handleSubmit = async () => {
    const requestData = {
      Resposta1,
      Resposta2,
      Resposta3,
      Resposta4,
      Resposta5
    };
    setCarregando(true);

    try {
      const response = await axios.post('https://localhost:7133/api/SolicitacaoFront', requestData);
      setRespostaBackend(response.data.url); // Define RespostaBackend com o valor da URL recebida
      setExistPlaylist("Criar outra")
      alert("Dados enviados com sucesso!");
    } catch (error) {
      alert("Erro ao enviar dados");
      console.error("Erro:", error);
    }
    
    setCarregando(false);
  };

  return (
    <div>
      <div>
        <p>Como você está se sentindo? </p>
        
        <input type="range" min="0" max="100" value={Resposta1} onChange={(e) => setResposta1(Number(e.target.value))} />
        <input type="range" min="0" max="100" value={Resposta2} onChange={(e) => setResposta2(Number(e.target.value))} />
        <input type="range" min="0" max="100" value={Resposta3} onChange={(e) => setResposta3(Number(e.target.value))} />
      </div>

      <div>
        <p>O que você está fazendo?</p>
        <button onClick={() => setResposta4('Relaxando')}>Relaxando</button>
        <button onClick={() => setResposta4('Estudando')}>Estudando</button>
        <button onClick={() => setResposta4('Trabalhando')}>Trabalhando</button>
        <button onClick={() => setResposta4('Dirigindo')}>Dirigindo</button>
        <button onClick={() => setResposta4('Comendo')}>Comendo</button>
        <button onClick={() => setResposta4('Treinando')}>Treinando</button>
        <button onClick={() => setResposta4('Fumando')}>Fumando</button>
      </div>

      <div>
        <p>Uma playlist que?</p>
        <button onClick={() => setResposta5('Match')}>Match</button>
        <button onClick={() => setResposta5('Clash')}>Clash</button>
      </div>

      {Carregando ? (
        <p>Carregando...</p>
      ) : ( 
        <button onClick={handleSubmit}>{ExistPlaylist}</button>
      )}

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
