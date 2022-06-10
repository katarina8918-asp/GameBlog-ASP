using GameBlog.DataAccess;
using GameBlog.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameBlog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        public string HashPassword(string password)
        {
            string myPassword = password;
            string mySalt = BCrypt.Net.BCrypt.GenerateSalt();
            string myHash = BCrypt.Net.BCrypt.HashPassword(myPassword, mySalt);

            return myHash;
        }

        // POST api/<DataController>
        [HttpPost]
        public IActionResult Post()
        {
            var context = new Context();

            if (context.Users.Any())
            {
                return Conflict();
            }

            var categories = new List<Category>
            {
                new Category{ Name = "News" },
                new Category{ Name = "Tips & Guides" },
                new Category{ Name = "Reviews" },
                new Category{ Name = "The Bests" },
                new Category{ Name = "Culture" },
                new Category{ Name = "Reports" },
                new Category{ Name = "Opinions" }
            };
            var users = new List<User>
            {
                new User
                {
                    FirstName = "Sam",
                    LastName = "Smith",
                    UserName = "sam123",
                    Email = "samsmith@gmail.com",
                    Password = HashPassword("Pass123!")
                },
                new User
                {
                    FirstName = "John",
                    LastName = "Johnson",
                    UserName = "john.jo",
                    Email = "johnjohnson@gmail.com",
                    Password = HashPassword("Pass123!")
                },
                new User
                {
                    FirstName = "Will",
                    LastName = "Williams",
                    UserName = "will",
                    Email = "will@gmail.com",
                    Password = HashPassword("Pass123!")
                },
                new User
                {
                    FirstName = "James",
                    LastName = "Jones",
                    UserName = "james",
                    Email = "jamesjones@gmail.com",
                    Password = HashPassword("Pass123!")
                },
                new User
                {
                    FirstName = "Mary",
                    LastName = "Mills",
                    UserName = "mary",
                    Email = "mary@gmail.com",
                    Password = HashPassword("Pass123!")
                },
                new User
                {
                    FirstName = "Sarah",
                    LastName = "Lee",
                    UserName = "sarah",
                    Email = "sarahlee@gmail.com",
                    Password = HashPassword("Pass123!")
                },
                new User
                {
                    FirstName = "Emily",
                    LastName = "King",
                    UserName = "emking",
                    Email = "emilyking@gmail.com",
                    Password = HashPassword("Pass123!")
                },
                new User
                {
                    FirstName = "Paul",
                    LastName = "Allen",
                    UserName = "pallen",
                    Email = "paulallen@gmail.com",
                    Password = HashPassword("Pass123!")
                },
                new User
                {
                    FirstName = "Lisa",
                    LastName = "Adams",
                    UserName = "lisadams",
                    Email = "lisadams@gmail.com",
                    Password = HashPassword("Pass123!")
                },
                new User
                {
                    FirstName = "Michelle",
                    LastName = "Hall",
                    UserName = "mhall",
                    Email = "mhall@gmail.com",
                    Password = HashPassword("Pass123!")
                },
                new User
                {
                    FirstName = "Steph",
                    LastName = "Roberts",
                    UserName = "robb",
                    Email = "steph@gmail.com",
                    Password = HashPassword("Pass123!")
                },
                new User
                {
                    FirstName = "Miles",
                    LastName = "Morales",
                    UserName = "milesm",
                    Email = "milesmo@gmail.com",
                    Password = HashPassword("Pass123!")
                },
                new User
                {
                    FirstName = "Gary",
                    LastName = "Cook",
                    UserName = "gary",
                    Email = "garycook@gmail.com",
                    Password = HashPassword("Pass123!")
                },
                new User
                {
                    FirstName = "Scott",
                    LastName = "Lee",
                    UserName = "scottlee",
                    Email = "scottlee@gmail.com",
                    Password = HashPassword("Pass123!")
                },
                new User
                {
                    FirstName = "Janet",
                    LastName = "Cooper",
                    UserName = "jane",
                    Email = "janetcooper@gmail.com",
                    Password = HashPassword("Pass123!")
                },
                new User
                {
                    FirstName = "Tyler",
                    LastName = "Sanders",
                    UserName = "tsanders",
                    Email = "tsanders@gmail.com",
                    Password = HashPassword("Pass123!")
                },
                new User
                {
                    FirstName = "Regular",
                    LastName = "User",
                    UserName = "regularuser",
                    Email = "regularuser@gmail.com",
                    Password = HashPassword("Pass123!")
                },//17
                new User
                {
                    FirstName = "Admin",
                    LastName = "User",
                    UserName = "adminuser",
                    Email = "adminuser@gmail.com",
                    Password = HashPassword("Pass123!")
                }//18
            };
            var posts = new List<Post>
            {
                new Post
                {
                    Title ="Diablo Immortal Won’t Launch In Some Countries Due To Loot Box Laws",
                    Text ="Well, here’s some bad news for people who live in Belgium or the Netherlands and are excited to play Diablo Immortal, Blizzard’s upcoming free-to-play dungeon crawler out June 2 on PC and mobile devices. " +
                    "It turns out, due to local loot box laws, that Blizzard’s next action RPG won’t be released in either country.And it seems unlikely that Blizzard will change the game soon to comply with these local laws, leaving affected players without many options.As first reported by Dutch news site Tweakers, Activision Blizzard has quietly shifted its release plans with Diablo Immortal. ",
                    User = users.First(),
                    Category = categories.First()
                },
                new Post
                {
                    Title ="Star Wars Jedi: Fallen Order Sequel Coming Next Year, Will Be Next-Gen Only",
                    Text ="Star Wars Jedi: Survivor, the highly-anticipated sequel to 2019's Star Wars Jedi: Fallen Order was finally revealed today via a new teaser trailer during Star Wars Celebration. It will be released in 2023 for PC, PS5, and Xbox Series X/S. While we knew a sequel was in the works, this is the first time EA and Lucasfilm have confirmed the rumored name, which leaked earlier this year.Star Wars Jedi: Survivor is the follow-up to Respawn’s 2019 third-person action-adventure Star Wars game, Fallen Order. Like that game, this new sequel stars Cal Kestis, a former Jedi who is on the run from the Empire alongside a ragtag group of misfits and outsiders. Y’know, classic Star Wars stuff.",
                    User = users.First(),
                    Category = categories.First()
                },
                new Post
                {
                    Title ="V Rising's Huge Success On Steam Has Taken Even Its Developers By Surprise",
                    Text ="V Rising came out on May 18, and I would bet that on the day almost none of you would have ever heard of it. One week later and it’s sold over 500,000 copies, and as I type this is being played by more people on Steam than Elden Ring. So what’s V Rising? It’s a vampire game where you get to create your own character and do some fighting. More specifically—and you’ll have to bear with me on this—it’s a vampire game that is a little bit of loads of other games all rolled into one. There’s some Diablo there. But also some base - building.Some survival stuff. Adventuring in an open world.And it supports co-op but also competitive multiplayer. Oh, and it’s still in Early Access.",
                    User = users.ElementAt(1),
                    Category = categories.First()
                },
                new Post
                {
                    Title ="Spider-Man Could Have Been An Xbox Exclusive",
                    Text ="It’s wild to think about a world in which Insomniac didn’t make one of PlayStation’s fastest-selling games of all time, and yet it very nearly happened. After cutting a decade-long partnership with Activision, Marvel Games had been shopping the Spider-Man IP to other major publishers. Taking on an external IP didn’t fit with Microsoft’s business strategy at the time, and they declined. Sony took a gamble by investing in Spider-Man, and it paid off.As originally spotted by VGC, the book The Ultimate History of Video Games, Volume 2 details how Spider-Man ended up becoming a Sony exclusive. Though Activision had been publishing Spider-Man games for almost 14 years, Jay Ong, the executive vice president of Marvel Games, felt that the publisher wasn’t doing enough with the IP. He noted that The Amazing Spider-Man 2, which had released in 2014, had scored in the 40s on Metacritic.",
                    User = users.ElementAt(2),
                    Category = categories.First()
                },
                new Post
                {
                    Title ="How To Actually Buy A PS5 (Probably)",
                    Text ="Trying to get your hands on a PlayStation 5 remains a fool’s errand. Stock sells out as soon as it becomes available. When it pops up from unofficial retailers, it does so with a staggering markup.The scarcity is driven by a bunch of factors: by bots, which scoop up consoles en masse automatically; by a chip production shortage that’s resulted in disruptions across industries; by a pandemic that upended the global supply line; by scalpers, who are just the worst. Experts believe that typical buying conditions—in which you can pop into your nearest GameStop and pick a PS5 up off the shelf—won’t be seen until 2023, at the very earliest.",
                    User = users.ElementAt(2),
                    Category = categories.ElementAt(1)
                },
                new Post
                {
                    Title ="Keep Your Elden Ring Spark Alive With These 7 Electrifying, Distinct Games",
                    Text ="You lay your final blows on the galactic Elden Beast and welcome in an age of your choosing to the troubled Lands Between. The screen fades to black, the credits start to rain down, and as you listen to Elden Ring’s full-bodied orchestra one last time, you know that you did good. Alternatively, you got bored of dying to Caelid’s scab-headed giant birds, and are now ready for something, anything to play instead.You’re in luck, buddy. FromSoftware’s open-world epic will likely never get old, but the games you left at the bottom of a drawer somewhere (or haven’t yet picked up) are banging on your door, begging you for just one, measly chance.",
                    User = users.ElementAt(3),
                    Category = categories.ElementAt(1)
                },
                new Post
                {
                    Title ="Codes To Unlock Secret Characters And Ships In Lego Star Wars: The Skywalker Saga",
                    Text ="There are approximately 40,000 playable characters and ships in the new Lego Star Wars game. (Editor’s note: No, there’s only like 350.) But most of them are locked when you start, and while completing all of the story missions will unlock some of these heroes and villains in The Skywalker Saga, you’ll still need to work to acquire most of the others. Luckily, there exist a few codes for Skywalker Saga that can speed up the process and will also unlock some of the game’s more silly or strange characters.",
                    User = users.ElementAt(3),
                    Category = categories.ElementAt(1)

                },
                new Post
                {
                    Title ="This Is As Close To A Real-World Escape Room As You'll Find",
                    Text ="I have found my happy place! Escape Simulator is such a lovely thing, a first-person simulacrum of escape rooms, built in 3D, with realistic physics. It is, as its title suggests, a simulation of attending a real-world escape room, in a way that almost all room-escape video games are not. Apart from when it’s in space. Let me clarify. I absolutely love throwaway room escape games, with their daft puzzles, ridiculous attempts at storylines, and deeply peculiar obsession with chucking away every useful tool after being used just once. I love the actually good ones even more, and none are better than Rusty Lake’s Cube series. But also, none of these is anything like playing in a real-life escape room.",
                    User = users.ElementAt(4),
                    Category = categories.ElementAt(2)
                },
                new Post
                {
                    Title ="Kirby And The Forgotten Land",
                    Text ="Despite its semi-open-world setting, the game doesn’t get swallowed by the vastness of its new format. For clarity, Forgotten Land is not technically an open-world game as its areas are segmented—but for the sake of brevity, we will describe it as such. Forgotten Land is intuitive without holding your hand, allows you to explore the gameplay the way you want, and is stuffed with a ton of formidable enemies and hidden environmental details in between. One day while chilling in Dream Land, a rift tears open in the sky and sucks up Waddle Dees. In an attempt to rescue the Waddle Dees, our hero Kirby winds up getting isekai’d into a dystopian world that resembles our own.",
                    User = users.ElementAt(5),
                    Category = categories.ElementAt(2)
                },
                new Post
                {
                    Title ="Total War: Warhammer III",
                    Text ="Time and a succession of good video games have proven me very, very wrong. About the first point, at least. The first Total War: Warhammer was released all the way back in 2016, and was pretty great, if also a bit of an odd duck seeing as it was the studio’s first real foray into non-historical Total War settings. The second, however, was a revelation. As big, fresh and fun as a Total War game had ever been, or really, has been since.",
                    User = users.ElementAt(5),
                    Category = categories.ElementAt(2)
                },
                new Post
                {
                    Title ="All The Big Pokémon Games, Ranked From Worst To Best",
                    Text ="Pokémon. It’s the biggest multimedia franchise on the planet. Everyone knows what a Pikachu is. The Pokéball, a red-and-white sphere bisected by a black line, is immediately recognizable to even your most out-of-touch aunts and uncles. But it all started with a game: 1996’s Pokémon Red and Blue versions, for Nintendo’s original Game Boy.",
                    User = users.ElementAt(6),
                    Category = categories.ElementAt(3)
                },
                new Post
                {
                    Title ="Every Borderlands Game, Ranked From Worst To Best",
                    Text ="Once upon a time, it seemed like Borderlands could’ve been just a fad. But between the hot new game, the hotly anticipated upcoming game, and the forthcoming silver screen treatment, it’s safe to say Borderlands is having its biggest year ever. The series is now cemented in video game canon.",
                    User = users.ElementAt(6),
                    Category = categories.ElementAt(3)
                },
                new Post
                {
                    Title ="New Pokémon GO Card Is Really Ditto In Disguise",
                    Text ="The upcoming Pokémon GO expansion for the trading card game includes Melmetal VMAX, the original legendary birds, and a wild new Ditto transformation that is unlike anything the TCG has ever seen before. I’m sure it will be extremely rare to find and also incredibly satisfying for those that do.There have been tons of Ditto cards before, but the one coming July 1 when the Pokémon GO expansion drops is special because it uses stickers to disguise itself as other Pokémon.",
                    User = users.ElementAt(7),
                    Category = categories.ElementAt(4)
                },
                new Post
                {
                    Title ="Recent Games To Play When You Want A Great Story Or Characters",
                    Text ="Games rooted in abstract systems or that see you controlling vast armies of faceless soldiers can be great, but sometimes you want something that feels a little more intimate and human. Something where the characters are so lively, playing the game almost feels like hanging out with friends, or maybe an experience that offers piercing insight into our struggles with grief and trauma.",
                    User = users.ElementAt(7),
                    Category = categories.ElementAt(4)
                },
                new Post
                {
                    Title ="WarioWare Finally Returns With Get It Together",
                    Text ="Mario’s lovably disgusting foil Wario hasn’t had a new game in quite a while. Nintendo fixes that September 10 with WarioWare: Get It Together, featuring the same fast-paced mini-games we know and love with support for two players at once.",
                    User = users.ElementAt(7),
                    Category = categories.ElementAt(4)
                },
                new Post
                {
                    Title ="Hangar 13 Bosses Leave, New Mafia In Development",
                    Text ="Hangar 13 studio head Haden Blackman is leaving the Mafia III maker after seven years, 2K Games announced Wednesday. The publisher wrote in an email to staff that the former LucasArts veteran is going to “pursue his passion at a new endeavor.” Kotaku understands that the move comes with a new Mafia game very early in development at Hangar 13, according to a source familiar with the plans.",
                    User = users.ElementAt(8),
                    Category = categories.ElementAt(5)
                },
                new Post
                {
                    Title ="Halo Infinite Just Can’t Catch A Break",
                    Text ="Busted challenges. A dysfunctional battle pass. A bunch of ultimately ineffectual “fixes.” A cacophonous uproar of player feedback, much of which isn’t positive. It may sound like the launch-window era of Halo Infinite, but no: This is Halo Infinite today, six months after its launch, during the first week of its second season.",
                    User = users.ElementAt(8),
                    Category = categories.ElementAt(5)
                },
                new Post
                {
                    Title ="Horizon Forbidden West Dev Dishes On The Game’s Biggest Twist",
                    Text ="Partway through Horizon Forbidden West, you encounter a monster. It’s not an animal-shaped robot, like the bulk of enemies you fight. It’s a bona fide creature of the shadows, and a rare moment of terror in a series that largely avoids jump scares and nightmare fuel. According to the game’s narrative director, the moment was a frictionless creative choice in development.",
                    User = users.ElementAt(9),
                    Category = categories.ElementAt(5)
                },
                new Post
                {
                    Title ="Valve Made Steam Deck Easy To Mod And Repair, And It’s Starting To Pay Off",
                    Text ="As reported by The Verge over the weekend, the legendary repair advocates over at iFixit.com plan to offer essentially every part of the Steam Deck for sale, including the motherboard with its custom AMD chip. While word of this was undoubtedly exciting for Steam Deck fans, iFixit took to Twitter to state that the news was a little bit premature and that a full reveal of its replacement parts offerings is yet to come.",
                    User = users.ElementAt(9),
                    Category = categories.ElementAt(6)
                },
                new Post
                {
                    Title ="Cursed Mario Kart Clone Starring Peter Griffin Is Actually Pretty Good",
                    Text ="I was horrified by the trailer for Warped Kart Racers, a new kart-racing game that blends together several popular animated shows. But like a terrible car wreck, I couldn’t look away from the waking nightmare of Peter Griffin and Hank Hill racing each other in a Mario Kart clone. It was cursed. And yet, now that I’ve actually played it, I’m even more horrified to tell you all that it’s pretty good, actually.",
                    User = users.ElementAt(9),
                    Category = categories.ElementAt(6)
                },
                new Post
                {
                    Title ="Beautiful Stardew Valley-Style RPG Is The Best Farming Game I’ve Played In A Decade",
                    Text ="Last Friday, when I started playing Steam Early Access farming sim Immortal Life, I was only planning to play for a couple of hours before bed. I finally ended my session at 5 a.m. It’s a standout among farming sims because it tries to emulate the verisimilitude of life instead of the dreary economics of running a farm. Immortal Life has a beautiful art style, the villagers are all intensely likable, and the farming systems are satisfying without feeling punishing. Though the game is still only in early access, Immortal Life is my favorite farming simulation game since 2009’s Rune Factory 3 on Nintendo DS.",
                    User = users.ElementAt(10),
                    Category = categories.ElementAt(6)
                },

            };
            var likes = new List<Like>
            {
                new Like
                {
                    Post = posts.First(),
                    User = users.ElementAt(10),
                },
                new Like
                {
                    Post = posts.First(),
                    User = users.ElementAt(11),
                },
                new Like
                {
                    Post = posts.First(),
                    User = users.ElementAt(5),
                },
                new Like
                {
                    Post = posts.ElementAt(1),
                    User = users.ElementAt(2),
                },
                new Like
                {
                    Post = posts.ElementAt(2),
                    User = users.ElementAt(10),
                },
                new Like
                {
                    Post = posts.ElementAt(4),
                    User = users.ElementAt(13),
                },
                new Like
                {
                    Post = posts.ElementAt(5),
                    User = users.ElementAt(3),
                },
                new Like
                {
                    Post = posts.ElementAt(2),
                    User = users.ElementAt(15),
                },
                new Like
                {
                    Post = posts.ElementAt(7),
                    User = users.ElementAt(8),
                },
                new Like
                {
                    Post = posts.ElementAt(5),
                    User = users.ElementAt(12),
                },
                new Like
                {
                    Post = posts.ElementAt(10),
                    User = users.ElementAt(6),
                },
                new Like
                {
                    Post = posts.ElementAt(6),
                    User = users.ElementAt(0),
                },
                new Like
                {
                    Post = posts.ElementAt(6),
                    User = users.ElementAt(1),
                },
                new Like
                {
                    Post = posts.ElementAt(18),
                    User = users.ElementAt(10),
                },
                new Like
                {
                    Post = posts.ElementAt(15),
                    User = users.ElementAt(9),
                },
                new Like
                {
                    Post = posts.ElementAt(9),
                    User = users.ElementAt(3),
                },
                new Like
                {
                    Post = posts.ElementAt(9),
                    User = users.ElementAt(1),
                },
                new Like
                {
                    Post = posts.ElementAt(11),
                    User = users.ElementAt(6),
                }
            };
            var comments = new List<Comment>
            {
                new Comment
                {
                    Text = "Comment 1",
                    User = users.ElementAt(10),
                    Post = posts.First()
                },
                new Comment
                {
                    Text = "Comment 2",
                    User = users.ElementAt(3),
                    Post = posts.ElementAt(1)
                },
                new Comment
                {
                    Text = "Comment 3",
                    User = users.ElementAt(10),
                    Post = posts.ElementAt(1)
                },
                new Comment
                {
                    Text = "A comment on a post 1",
                    User = users.ElementAt(10),
                    Post = posts.ElementAt(2)
                },
                new Comment
                { 
                    Text = "A comment on a post 2",
                    User = users.ElementAt(1),
                    Post = posts.ElementAt(2)
                },
                new Comment
                {
                    Text = "A comment on a post 3",
                    User = users.ElementAt(7),
                    Post = posts.ElementAt(5)
                },
                new Comment
                {
                    Text = "Another comm",
                    User = users.ElementAt(12),
                    Post = posts.ElementAt(0)
                },
                new Comment
                {
                    Text = "Nice",
                    User = users.ElementAt(13),
                    Post = posts.ElementAt(6)
                },
                new Comment
                {
                    Text = "Great",
                    User = users.ElementAt(5),
                    Post = posts.ElementAt(0)
                },
                new Comment
                {
                    Text = "This is another comment",
                    User = users.ElementAt(3),
                    Post = posts.ElementAt(18)
                }
            };

            //user
            var userUseCases = Enumerable.Range(2, 14).ToList();
            //admin
            var adminUseCases = Enumerable.Range(2, 24).ToList();

            userUseCases.ForEach(x => context.UserUseCases.Add(new UserUseCase
            {
                UserUseCaseId = x,
                User = users.ElementAt(16)
            }));
            adminUseCases.ForEach(x => context.UserUseCases.Add(new UserUseCase
            {
                UserUseCaseId = x,
                User = users.ElementAt(17)
            }));

            context.Categories.AddRange(categories);
            context.Users.AddRange(users);
            context.Posts.AddRange(posts);
            context.Likes.AddRange(likes);
            context.Comments.AddRange(comments);

            context.SaveChanges();
            return StatusCode(201);
        }


        
    }
}
