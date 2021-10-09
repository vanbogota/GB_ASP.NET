using Dapper;
using MetricsManager.Controllers;
using MetricsManager.Interfaces;
using MetricsManager.Servicies;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Repositories
{
    public class AgentsRepository : IAgentsRepository
    {
        private const string ConnectionString = "Data Source=metricsmanager.db;Version=3;Pooling=true;Max Pool Size=100;";
        public AgentsRepository()
        {
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }
        public void Create(AgentInfo agent)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("INSERT INTO agents(agentid, agentadress) VALUES(@agentid, @agentadress)",
                    new
                    {
                        agentid = agent.AgentId,
                        agentadress = agent.AgentAdress
                    });
            }
        }

        public void Delete(int agentId)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("DELETE FROM agents WHERE agentid=@agentId",
                    new
                    {
                        agentid = agentId
                    });
            }
        }

        public IList<AgentInfo> GetAll()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<AgentInfo>("SELECT AgentId, AgentAdress FROM agents").ToList();
            }
        }

        public AgentInfo GetById(int agentId)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.QuerySingle<AgentInfo>("SELECT AgentId, AgentAdress FROM agents WHERE agentid=@agentId",
                    new
                    {
                        agentid = agentId
                    });
            }
        }

        public IList<AgentInfo> GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime)
        {
            throw new NotImplementedException("Недопустимая операция");
        }

        public void Update(AgentInfo agent)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("UPDATE agents SET agentadress = @agentadress WHERE agentid=@agentId",
                    new
                    {
                        agentadress = agent.AgentAdress,
                        agentid = agent.AgentId
                    });
            }
        }
    }
}
