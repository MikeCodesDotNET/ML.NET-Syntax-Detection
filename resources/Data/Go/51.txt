package napodate

import "context"

// Service provides some "date capabilities" to your application
type Service interface {
    Status(ctx context.Context) (string, error)
    Get(ctx context.Context) (string, error)
    Validate(ctx context.Context, date string) (bool, error)
}