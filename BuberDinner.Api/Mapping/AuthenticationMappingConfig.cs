using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Contracts.Authentication;
using Mapster;

namespace BuberDinner.Api.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>()
            .Map(dest => dest.FirstName, src => src.first_name)
            .Map(dest => dest.LastName, src => src.last_name)
            .Map(dest => dest.Email, src => src.email)
            .Map(dest => dest.Password, src => src.password);
            
        config.NewConfig<LoginRequest, LoginQuery>()
            .Map(dest => dest.Email, src => src.email)
            .Map(dest => dest.Password, src => src.password);

        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest, src => src.User);
    }
}