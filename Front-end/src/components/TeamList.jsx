import React, { useEffect, useState } from 'react';
import axios from 'axios';
import PlayerCard from './PlayerCard';

export default function TeamList() {
    const [teams, setTeams] = useState([]);
    const [loading, setLoading] = useState(true);
    const [selectedTeam, setSelectedTeam] = useState(null);
    const [players, setPlayers] = useState([]);
    const [loadingPlayers, setLoadingPlayers] = useState(false);

    useEffect(() => {
        axios.get('http://localhost:5115/times')
            .then(response => {
                setTeams(response.data);
                setLoading(false);
            })
            .catch(error => {
                console.error("Erro ao buscar times:", error);
                setLoading(false);
            });
    }, []);

    const handleTeamClick = (team) => {
        setSelectedTeam(team);
        setLoadingPlayers(true);
        setPlayers([]); // clear previous

        axios.get(`http://localhost:5115/api/jogadores/${team.id}`)
            .then(response => {
                setPlayers(response.data);
                setLoadingPlayers(false);
            })
            .catch(error => {
                console.error("Erro ao buscar jogadores:", error);
                setLoadingPlayers(false);
            });
    };

    if (loading) return <div style={{ color: 'white', textAlign: 'center' }}>Carregando times...</div>;

    return (
        <section id="times" style={styles.section}>
            <h3 style={styles.heading}>
                Os times <span style={{ color: '#FAEC81' }}>brasileiros</span> mais acessados
            </h3>

            <div style={styles.grid}>
                {teams.map(team => (
                    <div
                        key={team.id}
                        style={{ ...styles.card, border: selectedTeam?.id === team.id ? '2px solid #FAEC81' : 'none' }}
                        onClick={() => handleTeamClick(team)}
                    >
                        <img
                            src={team.escudo}
                            alt={team.nome}
                            style={styles.logo}
                            title={team.nome}
                        />
                    </div>
                ))}
            </div>

            {selectedTeam && (
                <div style={styles.playersSection}>
                    <h2 style={{ color: 'white', marginBottom: '30px', marginTop: '30px' }}>
                        Jogadores do <span style={{ color: '#FD5656' }}>{selectedTeam.nome}</span>
                    </h2>
                    {loadingPlayers ? (
                        <p style={{ color: '#FAEC81' }}>Carregando elenco...</p>
                    ) : (
                        <div style={styles.grid}>
                            {players.map(player => (
                                <PlayerCard key={player.id} player={player} />
                            ))}
                            {players.length === 0 && <p style={{ color: 'white' }}>Nenhum jogador encontrado.</p>}
                        </div>
                    )}
                </div>
            )}
        </section>
    );
}

const styles = {
    section: {
        padding: '50px 20px',
        textAlign: 'center',
        marginTop: '50px', // Reduced from 250px
        minHeight: '400px' // Ensure visibility
    },
    heading: {
        fontSize: '30px',
        marginBottom: '40px',
        color: '#FFFF'
    },
    grid: {
        display: 'flex',
        justifyContent: 'center',
        flexWrap: 'wrap',
        gap: '20px'
    },
    card: {
        width: '80px',
        height: '80px',
        backgroundColor: '#171C3D',
        borderRadius: '15px',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        cursor: 'pointer',
        transition: 'transform 0.2s, border 0.2s'
    },
    logo: {
        width: '60%',
        maxWidth: '50px'
    },
    playersSection: {
        marginTop: '50px',
        borderTop: '1px solid #333',
        paddingTop: '30px',
        animation: 'fadeIn 0.5s'
    }
};
