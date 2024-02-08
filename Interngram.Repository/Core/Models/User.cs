using System;
using System.ComponentModel.DataAnnotations;

namespace Interngram.Repository.Models;

public class User
{
    [Key]
    public string Id { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string NickName { get; set; } = null!;
    public DateTime BirthDay { get; set; }
    public string City { get; set; } = null!;
    public string Bio { get; set; } = null!;
    public string Avatar { get; set; } = null!;

    public List<string> Subscribers { get; set; } = new List<string>();
    public List<string> Subscriptions { get; set; } = new List<string>();
}